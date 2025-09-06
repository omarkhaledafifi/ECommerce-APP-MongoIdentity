using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs;
using Shared.DTOs.Products;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper) : IProductService
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
            return mapper.Map<Product, ProductResponse>(data);
        }

        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var product = mapper.Map<CreateProductRequest, Product>(request);
            product.PictureUrl = await imageHelper.SaveImageAsync(request.PictureUrl, "Products");
            await repo.AddAsync(product);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Product, ProductResponse>(product);
        }

        // ---------------- UPDATE ----------------
        public async Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var entity = await repo.GetAsync(id);

            if (entity == null)
                return null;

            if(!(request.PictureUrl == null || request.PictureUrl.Length == 0) && imageHelper.DeleteImage(entity.PictureUrl))
                entity.PictureUrl = await imageHelper.SaveImageAsync(request.PictureUrl, "Products");
            mapper.Map(request, entity);

            repo.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Product, ProductResponse>(entity);
        }

        // ---------------- DELETE ----------------
        public async Task<bool> DeleteProductAsync(int id)
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var entity = await repo.GetAsync(id);

            if (entity == null)
                return false;

            imageHelper.DeleteImage(entity.PictureUrl);

            repo.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
