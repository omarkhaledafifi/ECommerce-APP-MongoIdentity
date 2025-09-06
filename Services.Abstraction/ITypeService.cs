using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ITypeService
    {
        //GetTypes
        Task<IEnumerable<TypeResponse>> GetTypesAsync();
    }
}
