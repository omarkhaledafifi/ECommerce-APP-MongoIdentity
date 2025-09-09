using MediatR;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CQRS.Product
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductResponse>;
}
