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
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id) : base(product => product.Id == id)
        {
            AddProductIncludes();
        }
        public ProductWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base( p =>
            (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) && 
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || (p.ProductName.ToLower().Contains(parameters.Search.ToLower()))))
        {
            AddProductIncludes();
            AddSorting(parameters.Options);
            AddPagination(parameters.PageSize, parameters.PageIndex);

        }

        private void AddSorting(ProductSortingOptions options)
        {
            switch (options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.ProductName);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.ProductName);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }

        private void AddProductIncludes()
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }
    }
}
