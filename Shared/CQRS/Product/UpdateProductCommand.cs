using MediatR;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CQRS.Product
{
    public record UpdateProductCommand(int Id,
    string ProductName,
    decimal Price,
    string Description,
    string PictureUrl,
    int BrandId,
    int TypeId) : IRequest<ProductResponse>;
}
