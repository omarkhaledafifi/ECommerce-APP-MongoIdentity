using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Products
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile PictureUrl { get; set; }
        public int BrandId { get; set; }
        public int TypeId { get; set; }
    }
}
