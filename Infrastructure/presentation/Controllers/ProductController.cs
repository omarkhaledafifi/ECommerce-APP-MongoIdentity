using Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Products;
using Shared;
using Shared.DTOs;
using MediatR;
using Shared.CQRS.Product;
using Services.Abstraction.CQRS;

namespace presentation.Controllers
{
    public class ProductsController(IServiceManager serviceManager,
        IMediator mediator,
        ICreateProductCommandOrchestrator createProductOrchestrator,
        IUpdateProductCommandOrchestrator updateProductOrchestrator,
        IDeleteProductCommandOrchestrator deleteProductOrchestrator) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters) //GET   BaseUrl/api/Products
        {
            var products = await mediator.Send(new GetProductsQuery(parameters));
            //await serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProduct(int id) //GET   BaseUrl/api/Products/18
        {
            var product = await mediator.Send(new GetProductByIdQuery(id));
            //await serviceManager.ProductService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromForm] CreateProductRequest request) //POST   BaseUrl/api/Products
        {
            var product = await createProductOrchestrator.CreateProductAsync(request);
            //await serviceManager.ProductService.CreateProductAsync(request);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, [FromForm] UpdateProductRequest request) //PUT   BaseUrl/api/Products/18
        {
            var product = await updateProductOrchestrator.UpdateProductAsync(id, request);
            //await serviceManager.ProductService.UpdateProductAsync(id, request);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id) //DELETE   BaseUrl/api/Products/18
        {
            var result = await deleteProductOrchestrator.DeleteProductAsync(id);
            //await serviceManager.ProductService.DeleteProductAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
