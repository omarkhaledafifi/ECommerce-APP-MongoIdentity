using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.CQRS.Brand;
using Shared.DTOs.Products;

namespace presentation.Controllers
{
    public class BrandsController(IServiceManager serviceManager, IMediator mediator) : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands() //GET   BaseUrl/api/Brands
        {
            var brands = await mediator.Send(new GetBrandsQuery());
            return Ok(brands);
        }
    }
}
