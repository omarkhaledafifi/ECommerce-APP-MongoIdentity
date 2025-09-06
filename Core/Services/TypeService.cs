using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TypeService(IUnitOfWork unitOfWork, IMapper mapper) : ITypeService
    {
        public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();
            var data = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(data);
        }
    }
}
