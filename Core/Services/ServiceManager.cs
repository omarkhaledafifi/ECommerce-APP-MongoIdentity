using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IImageHelper imageHelper) : IServiceManager
    {
        private readonly Lazy<ProductService> _lazyProductService = new Lazy<ProductService>(() => new ProductService(unitOfWork, mapper, imageHelper));
        private readonly Lazy<BrandService> _lazyBrandService = new Lazy<BrandService>(() => new BrandService(unitOfWork, mapper));
        private readonly Lazy<TypeService> _lazyTypeService = new Lazy<TypeService>(() => new TypeService(unitOfWork, mapper));
        private readonly Lazy<AuthenticationService> _lazyAuthenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService(userManager));
        public IProductService ProductService => _lazyProductService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IBrandService BrandService => _lazyBrandService.Value;

        public ITypeService TypeService => _lazyTypeService.Value;
    }
}
