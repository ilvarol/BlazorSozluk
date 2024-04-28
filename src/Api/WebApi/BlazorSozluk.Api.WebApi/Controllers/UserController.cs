using BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using BlazorSozluk.Api.Application.Features.Queries.GetUserDetail;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlazorSozluk.Api.WebApi.Controllers;

public class UserController : BaseController
{
    private IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await mediator.Send(new GetUserDetailQuery(id));

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("UserName/{userName}")]
    public async Task<IActionResult> GetByUserName(string userName)
    {
        var user = await mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Confirm")]
    public async Task<IActionResult> ConfirmEmail(Guid id)
    {
        var guid = await mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });

        return Ok(guid);
    }

    [HttpPost]
    [Route("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
    {
        if (!command.UserId.HasValue)
            command.UserId = UserId;

        var guid = await mediator.Send(command);

        return Ok(guid);
    }
}
