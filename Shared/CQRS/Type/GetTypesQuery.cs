using MediatR;
using Shared.DTOs.Products;


namespace Shared.CQRS.Type
{
    public record GetTypesQuery : IRequest<IEnumerable<TypeResponse>>;
}
