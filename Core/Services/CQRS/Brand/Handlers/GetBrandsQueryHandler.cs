using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using Shared.CQRS.Brand;
using Shared.DTOs.Products;

namespace Services.CQRS.Brand.Handlers
{
    public class GetBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBrandsQuery, IEnumerable<BrandResponse>>
    {
        public async Task<IEnumerable<BrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<ProductBrand, int>();
            var data = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(data);
        }
    }


}
