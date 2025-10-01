using AutoMapper;
using MediatR;
using Services.Abstraction;
using Services.Abstraction.CQRS;
using Shared.CQRS.Product;
using Shared.DTOs.Products;
using Shared.RabbitMQ;

namespace Services.CQRS.Product.Orchestrators
{
    public class CreateProductCommandOrchestrator
        (
        IMediator mediator,
        IImageHelper imageHelper,
        IMapper mapper,
        IRabbitMQPublisherService rabbitMQPublisher) : ICreateProductCommandOrchestrator
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

            // Publish message to RabbitMQ
            var message = mapper.Map<ProductCreatedMessage>(product);
            string TextMessage = System.Text.Json.JsonSerializer.Serialize(message);
            await rabbitMQPublisher.PublishMessage("Product", "Key.Added", TextMessage);

            return mapper.Map<ProductResponse>(product);
        }
    }
}
