using MediatR;
using Microsoft.AspNetCore.Mvc;
using Starter.Application.Endpoints.Categories.Commands;
using Starter.Application.Endpoints.Categories.Queries;
using Starter.Application.ProductContract.Commands;

namespace Starter.API.Controllers
{
    [Route("starter/[controller]")]
    [ApiController]
    public class CategoryController(ISender _sender) : ControllerBase
    {

        [HttpPost(Name = "addCategory")]
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

        //Uses action name as route name
        [HttpGet("[action]")]
        //[HttpGet("GetById")]
        public async Task<ActionResult<GetCategoryByIdResult>> GetById(Guid query)
        {
            return Ok(await _sender.Send(new GetCategoryByIdQuery(query)));
        }


    }
}
