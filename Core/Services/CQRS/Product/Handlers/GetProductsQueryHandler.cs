using AutoMapper;
using Domain.Contracts;
using MediatR;
using Services.Specifications;
using Shared.CQRS.Product;
using Shared.DTOs;
using Shared.DTOs.Products;

namespace Services.CQRS.Product.Handlers;
public class GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetProductsQuery, PaginatedResponse<ProductResponse>>
{
    public async Task<PaginatedResponse<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var speccificatios = new ProductWithBrandAndTypeSpecifications(request.parameters);
        var repo = unitOfWork.GetRepository<Domain.Entities.Product, int>();
        var data = await repo.GetAllAsync(speccificatios);
        var mappedData = mapper.Map<IEnumerable<Domain.Entities.Product>, IEnumerable<ProductResponse>>(data);
        var pageCount = data.Count();
        var totalCount = await repo.CountAsync(new ProductCountSpecifications(request.parameters));
        return new(request.parameters.PageIndex, pageCount, totalCount, mappedData);
    }
}
    
