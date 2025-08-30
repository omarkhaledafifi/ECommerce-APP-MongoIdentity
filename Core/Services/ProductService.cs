using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            var speccificatios = new ProductWithBrandAndTypeSpecifications(parameters);
            var repo = unitOfWork.GetRepository<Product, int>();
            var data = await repo.GetAllAsync(speccificatios);
            var mappedData = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(data);
            var pageCount = data.Count();
            var totalCount = await repo.CountAsync(new ProductCountSpecifications(parameters));
            return new(parameters.PageIndex, pageCount, totalCount, mappedData);
        }

        public async Task<ProductResponse> GetProductAsync(int id)
        {
            var speccificatios = new ProductWithBrandAndTypeSpecifications(id);
            var data = await unitOfWork.GetRepository<Product, int>().GetAsync(speccificatios);
            //var data = await repo.GetAsync(id);
            return mapper.Map<Product, ProductResponse>(data);
        }

        public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
        {
            var repo = unitOfWork.GetRepository<ProductBrand, int>();
            var data = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(data);
        }

        public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();
            var data = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(data);
        }
    }
}
