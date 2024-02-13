using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;

public class DeleteEntryVoteCommand : IRequest<bool>
{
    public DeleteEntryVoteCommand()
    {
    }

    public DeleteEntryVoteCommand(Guid entryId)
    {
        EntryId = entryId;
    }

    public Guid EntryId { get; set; }

    public Guid UserId { get; set; }
}