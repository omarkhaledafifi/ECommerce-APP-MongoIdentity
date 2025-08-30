using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction;
using Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Products;
using Shared;
using Shared.DTOs;

namespace presentation.Controllers
{
    public class ProductsController(IServiceManager serviceManager) : BaseController
    {
        //GetAllProducts => IEnumrable<ProductResponse>
        //GetProduct
        //GetBrands
        //GetTypes
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters) //GET   BaseUrl/api/Products
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProduct(int id) //GET   BaseUrl/api/Products/18
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands() //GET   BaseUrl/api/Products/brands
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes() //GET   BaseUrl/api/Products/types
        {
            var types = await serviceManager.ProductService.GetTypesAsync();
            return Ok(types);
        }


    }
}
