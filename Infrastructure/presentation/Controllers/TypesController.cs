using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs.Products;

namespace presentation.Controllers
{
    public class TypesController(IServiceManager serviceManager) : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes() //GET   BaseUrl/api/Types
        {
            var types = await serviceManager.TypeService.GetTypesAsync();
            return Ok(types);
        }
    }
}
