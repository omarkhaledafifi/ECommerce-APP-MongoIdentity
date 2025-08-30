using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Products;

namespace Services.MappingProfiles
{
    internal class PictureResolver(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                return $"{configuration["BaseUrl"]}{source.PictureUrl}";
            return "";
        }
    }
}