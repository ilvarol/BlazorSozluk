using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactrory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                                            exchangeType: SozlukConstants.DefaultExchangeType,
                                            queueName: SozlukConstants.CreateEntryVoteQueueName,
                                            obj: new CreateEntryVoteEvent() 
                                            {
                                                EntryId = request.EntryId,
                                                CreatedBy = request.CreatedBy
                                            });

        return Task.FromResult(true);
    }
}
