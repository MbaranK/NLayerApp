using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Service
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitofWork unitofWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitofWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDTO<CategorywithProductsDTO>> GetSingleCategorybyIdwithProductAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategorybyIdwithProductAsync(categoryId); //Entity

            //Entity to DTO dönüşümü
            var categoryDto = _mapper.Map<CategorywithProductsDTO>(category);

            return CustomResponseDTO<CategorywithProductsDTO>.Success(200, categoryDto);
        }
    }
}
