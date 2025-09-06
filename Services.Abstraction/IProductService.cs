using Shared;
using Shared.DTOs;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //GetAllProducts => IEnumrable<ProductResponse>
        Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters);
        //GetProduct
        Task<ProductResponse> GetProductAsync(int id);
        Task<ProductResponse?> CreateProductAsync(CreateProductRequest request);
        Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request);
        Task<bool> DeleteProductAsync(int id);
        

    }
}
