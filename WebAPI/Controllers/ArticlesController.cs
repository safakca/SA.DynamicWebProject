using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Features.CQRS.Queries.Articles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin, Member")]
[AllowAnonymous]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticlesController(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = "Member")]
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetArticlesQueryRequest()));

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _mediator.Send(new GetArticleByIdQueryRequest(id)));

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteArticleCommandRequest(id)));

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateArticleCommandRequest request) => Created("", await _mediator.Send(request));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateArticleCommandRequest request) => Ok(await _mediator.Send(request));
}
