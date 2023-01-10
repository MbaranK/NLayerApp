using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    // Route işlemeri CustomBaseControllerda yapılıyor.
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{categoryId}")] //EF'nin mapleyebilmesi için Id isimleri eşleşmek zorunda.

        public async Task<IActionResult> GetSingleCategorybyIdwithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategorybyIdwithProductAsync(categoryId));
        }
    }
}
