using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.CQRS
{
    public interface IUpdateProductCommandOrchestrator
    {
        Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest request);
    }
}
