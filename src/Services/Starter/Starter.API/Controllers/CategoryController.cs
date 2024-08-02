using MediatR;
using Microsoft.AspNetCore.Mvc;
using Starter.Application.Categories.Commands.CreateCategory;
using Starter.Application.Categories.Queries.GetAllCatagories;
using Starter.Application.Categories.Queries.GetCategoryById;

namespace Starter.API.Controllers
{
    [Route("starter/[controller]")]
    [ApiController]
    public class CategoryController(ISender _sender) : ControllerBase
    {

        [HttpPost(Name = "addCategory")]
        public async Task<ActionResult<Guid>> Add(CreateCategoryCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoriesResult>> GetAll([FromQuery] GetAllCategoriesQuery query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<GetCategoryByIdResult>> GetById(Guid query)
        {
            return Ok(await _sender.Send(new GetCategoryByIdQuery(query)));
        }


    }
}
