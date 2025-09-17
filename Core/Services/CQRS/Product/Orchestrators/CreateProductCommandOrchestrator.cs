using AutoMapper;
using MediatR;
using Services.Abstraction;
using Services.Abstraction.CQRS;
using Shared.CQRS.Product;
using Shared.DTOs.Products;

namespace Services.CQRS.Product.Orchestrators
{
    public class CreateProductCommandOrchestrator(IMediator mediator, IImageHelper imageHelper, IMapper mapper)
        : ICreateProductCommandOrchestrator
    {
        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            var pictureUrl = await imageHelper.SaveImageAsync(request.PictureUrl, "Products");

            var product = await mediator.Send(new CreateProductCommand(
                request.ProductName,
                request.Price,
                request.Description,
                pictureUrl,
                request.BrandId,
                request.TypeId
                ));

            return mapper.Map<ProductResponse>(product);
        }
    }
}
