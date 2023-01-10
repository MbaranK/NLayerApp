using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        //Servicelerden dto dönüyoruz. Bu ilk Dto'yu oluşturup sonra burada tanımlıyoruz.
        Task<CustomResponseDTO<CategorywithProductsDTO>> GetSingleCategorybyIdwithProductAsync(int categoryId);
    }
}
