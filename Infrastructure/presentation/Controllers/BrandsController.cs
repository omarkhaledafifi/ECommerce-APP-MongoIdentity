using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs.Products;

namespace presentation.Controllers
{
    public class BrandsController(IServiceManager serviceManager) : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands() //GET   BaseUrl/api/Brands
        {
            var brands = await serviceManager.BrandService.GetBrandsAsync();
            return Ok(brands);
        }
    }
}
