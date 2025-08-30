using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>().ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.ProductBrand.BrandName))
                                                 .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.ProductType.TypeName))
                                                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProductName))
                                                 .ForMember(d => d.PictureUrl, opt => opt.MapFrom<PictureResolver>());
            CreateMap<ProductBrand, BrandResponse>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BrandName));
            CreateMap<ProductType, TypeResponse>().ForMember(d => d.Name, opt => opt.MapFrom(s => s.TypeName));
            
        }

    }

    
}
