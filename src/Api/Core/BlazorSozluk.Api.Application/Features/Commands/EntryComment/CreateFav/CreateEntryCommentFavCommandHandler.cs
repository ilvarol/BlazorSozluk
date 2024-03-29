﻿using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.EntryComment;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactrory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                                            exchangeType: SozlukConstants.DefaultExchangeType,
                                            queueName: SozlukConstants.CreateEntryCommentFavQueueName,
                                            obj: new CreateEntryCommentFavEvent() 
                                            {
                                                EntryCommentId = request.EntryCommentId,
                                                CreatedBy = request.UserId
                                            });

        return await Task.FromResult(true);
    }
}
