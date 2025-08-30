using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpecifications(ProductQueryParameters parameters) : BaseSpecification<Product>(p =>
            (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || (p.ProductName.ToLower().Contains(parameters.Search.ToLower()))))
    {

    }
}
