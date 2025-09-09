using MediatR;
using Shared.DTOs;
using Shared.DTOs.Products;

namespace Shared.CQRS.Product
{
    public record GetProductsQuery(ProductQueryParameters parameters) : IRequest<PaginatedResponse<ProductResponse>>;
}
