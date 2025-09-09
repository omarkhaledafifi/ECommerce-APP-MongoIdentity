using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.CQRS
{
    public interface ICreateProductCommandOrchestrator
    {
        Task<ProductResponse> CreateProductAsync(CreateProductRequest request);
    }
}
