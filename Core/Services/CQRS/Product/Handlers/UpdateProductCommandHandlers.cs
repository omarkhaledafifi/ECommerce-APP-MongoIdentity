using AutoMapper;
using Domain.Contracts;
using MediatR;
using Shared.CQRS.Product;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Product.Handlers
{
    public class UpdateProductCommandHandlers(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Domain.Entities.Product, int>();
            var product = new Domain.Entities.Product()
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                PictureUrl = request.PictureUrl,
                BrandId = request.BrandId,
                TypeId = request.TypeId
            };
            repo.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Domain.Entities.Product, ProductResponse>(product);
        }
    }
}
