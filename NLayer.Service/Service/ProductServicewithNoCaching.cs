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
    public class ProductServicewithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServicewithNoCaching(IGenericRepository<Product> repository, IUnitofWork unitofWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitofWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductswithCategory()
        {
            var products = await _productRepository.GetProductswithCategory();


            var productsDto = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsDto);
        }
    }
}
