using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBrandService BrandService { get; }
        public ITypeService TypeService { get; }
        public IAuthenticationService AuthenticationService { get; }
    }
}
