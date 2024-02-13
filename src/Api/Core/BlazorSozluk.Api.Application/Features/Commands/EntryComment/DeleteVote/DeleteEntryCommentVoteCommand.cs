using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommand : IRequest<bool>
{
    public DeleteEntryCommentVoteCommand(Guid entryCommentId)
    {
        EntryCommentId = entryCommentId;
    }

    public Guid EntryCommentId { get; set; }

    public Guid UserId { get; set; }
}
