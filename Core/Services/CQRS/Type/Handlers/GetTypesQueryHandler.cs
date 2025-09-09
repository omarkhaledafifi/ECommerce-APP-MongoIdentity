using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using Shared.CQRS.Type;
using Shared.DTOs.Products;


namespace Services.CQRS.Type.Handlers
{
    public class GetTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetTypesQuery, IEnumerable<TypeResponse>>
    {
        public async Task<IEnumerable<TypeResponse>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();
            var data = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(data);
        }
    }
}
