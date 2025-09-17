using AutoMapper;
using Domain.Contracts;
using MediatR;
using Services.Abstraction;
using Services.Abstraction.CQRS;
using Shared.CQRS.Product;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Product.Orchestrators
{
    public class UpdateProductCommandOrchestrator(IMediator mediator, IImageHelper imageHelper) : IUpdateProductCommandOrchestrator
    {
        public async Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            //var product = await unitOfWork.GetRepository<Domain.Entities.Product, int>().GetAsync(id);
            var product = await mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
                return null;
            if (!(request.PictureUrl == null || request.PictureUrl.Length == 0) && imageHelper.DeleteImage(product.PictureUrl))
                product.PictureUrl = await imageHelper.SaveImageAsync(request.PictureUrl, "Products");


            var updatedProduct = await mediator.Send(new UpdateProductCommand(
                id,
                string.IsNullOrEmpty(request.ProductName) ? product.Name : request.ProductName,
                request.Price <= 0 ? product.Price : request.Price,
                string.IsNullOrEmpty(request.Description) ? product.Description : request.Description,
                product.PictureUrl,
                request.BrandId <= 0 ? product.BrandId : request.BrandId,
                request.TypeId <= 0 ? product.TypeId : request.TypeId
                ));
            return updatedProduct;
        }
    }
}
