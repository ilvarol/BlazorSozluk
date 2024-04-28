using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    public class EntryController : BaseController
    {
        IMediator mediator;

        public EntryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetEntryDetailQuery(id, UserId));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
        {
            var result = await mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && String.IsNullOrEmpty(userName))
                userId = UserId;

            var result = await mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetEntries(int page, int pageSize)
        {
            var entries = await mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));

            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await this.mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await this.mediator.Send(command);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }
    }
}
