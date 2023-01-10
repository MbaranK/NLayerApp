using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //Controller sayfasında CreateActionResult döndürebilmek için  her seferinde Controller'da Custom response oluşturmaktansa direk repositoryde döndürdük. Bu sayede controller 'da tek satır kod ile işimizi hallediyoruz.
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductswithCategory();
    }
}
