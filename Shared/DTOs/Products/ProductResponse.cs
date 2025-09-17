using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Products
{
    public record ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }

    }
}
