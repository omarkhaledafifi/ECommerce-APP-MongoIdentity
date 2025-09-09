using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.CQRS.Type;
using Shared.DTOs.Products;

namespace presentation.Controllers
{
    public class TypesController(IServiceManager serviceManager, IMediator mediator) : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes() //GET   BaseUrl/api/Types
        {
            //var types = await serviceManager.TypeService.GetTypesAsync();
            var types = await mediator.Send(new GetTypesQuery());
            return Ok(types);
        }
    }
}
