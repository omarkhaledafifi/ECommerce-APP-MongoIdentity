using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Products;
using Shared.RabbitMQ;
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

            CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId));

            CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
            .ForMember(dest => dest.PictureUrl, opt => opt.Ignore());

            // Mapping for RabbitMQ message
            CreateMap<ProductResponse, ProductCreatedMessage>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "ProductCreatedMessage"))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));

        }

    }

    
}
