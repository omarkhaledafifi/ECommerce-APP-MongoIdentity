using MediatR;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CQRS.Brand
{
    public record GetBrandsQuery : IRequest<IEnumerable<BrandResponse>>;
}
