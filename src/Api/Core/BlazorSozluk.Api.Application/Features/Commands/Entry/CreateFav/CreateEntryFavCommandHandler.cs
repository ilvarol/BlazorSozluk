﻿using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.Entry;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactrory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                                            exchangeType: SozlukConstants.DefaultExchangeType,
                                            queueName: SozlukConstants.CreateEntryFavQueueName,
                                            obj: new CreateEntryFavEvent() 
                                            {
                                                EntryId = request.EntryId,
                                                CreatedBy = request.UserId
                                            });

        return Task.FromResult(true);
    }
}
