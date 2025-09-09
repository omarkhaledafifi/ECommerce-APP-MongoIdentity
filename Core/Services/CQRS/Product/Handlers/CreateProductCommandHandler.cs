using AutoMapper;
using Domain.Contracts;
using MediatR;
using Services.Abstraction;
using Shared.CQRS.Product;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.CQRS.Product.Handlers
{
    public class CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Domain.Entities.Product, int>();
            var product = new Domain.Entities.Product() 
            { 
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                PictureUrl = request.PictureUrl,
                BrandId = request.BrandId,
                TypeId = request.TypeId
            };
            await repo.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Domain.Entities.Product, ProductResponse>(product);
        }
    }
    
}
