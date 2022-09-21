using Application.Services.ApiPublisher.DtoModels.Test.Requests;
using Application.Services.ApiPublisher.DtoModels.Test.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var commandResult = await _mediator.Send(new DtoCreateTestRequest());

            return commandResult ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var queryResult = await _mediator.Send(new DtoGetTestRequest());

            return Ok(queryResult);
        }
    }
}