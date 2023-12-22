using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (existsUser is not null) 
            throw new DatabaseValidationException("User already exists!");

        var dbUser = mapper.Map<Domain.Models.User>(request);

        var rows = await userRepository.AddAsync(dbUser);

        // Email Changed/Created
        if (rows > 0) 
        {
            var @event = new UserEmailChangedEvent();//The prefix "@" enables the use of keywords as identifiers
            QueueFactrory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                exchangeType: SozlukConstants.DefaultExchangeType,
                                                queueName: SozlukConstants.UserEmailChangedQueueName,
                                                obj: @event);
        }

        return dbUser.Id;
    }
}
