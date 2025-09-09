using AutoMapper;
using Domain.Contracts;
using MediatR;
using Services.Specifications;
using Shared.CQRS.Product;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Product.Handlers
{
    public class GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(request.Id);
            var data = await unitOfWork.GetRepository<Domain.Entities.Product, int>().GetAsync(specifications);
            return mapper.Map<Domain.Entities.Product, ProductResponse>(data);
        }
    }
}
