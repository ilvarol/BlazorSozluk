using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

public class VoteController : BaseController
{
    private IMediator mediator;

    public VoteController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("Entry/{entryId}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var user = await mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId));

        return Ok(user);
    }

    [HttpPost]
    [Route("EntryComment/{entryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var user = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId));

        return Ok(user);
    }

    [HttpPost]
    [Route("DeleteEntryVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        var user = await mediator.Send(new DeleteEntryVoteCommand(entryId));

        return Ok(user);
    }

    [HttpPost]
    [Route("DeleteEntryCommentVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        var user = await mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId));

        return Ok(user);
    }
}
