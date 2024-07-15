using MediatR;
using Microsoft.AspNetCore.Mvc;
using Starter.Application.Endpoints.Categories.Commands;
using Starter.Application.Endpoints.Categories.Queries;

namespace Starter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender _sender) : ControllerBase
    {

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create(CreateCategoryCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoriesResult>> Get([FromQuery] GetCategoriesQuery query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<GetCategoryByIdResult>> GetById(Guid query)
        {
            return Ok(await _sender.Send(new GetCategoryByIdQuery(query)));
        }


    }
}
