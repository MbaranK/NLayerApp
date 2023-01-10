using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Service;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        //Controller referans olarak service katmanını alıyor. Bu yüzden Repository değil Service'i referans olarak alıyor.
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        //GET api/products/GetProductswithCategory bu türlü yapmak zorundayız. Diğer türlü All methodu ile çakışır.
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductswithCategory()
        {
            return CreateActionResult(await _service.GetProductswithCategory());
        }



        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync(); //Entity 

            var productsDto = _mapper.Map<List<ProductDTO>>(products.ToList()); //mapleme işlemi ile geriye productsdto'yu döneceğiz.

            //return Ok (CustomResponseDTO<List<ProductDTO>>.Success(200, productsDto)); CustomerBaseController da dönme işlemini hallettik. Bu şekilde yazmamıza gerek kalmadı.

            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDto));
            
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productsDto));
        }

        //Add
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto)); //ProductDto'yu producta dönüştürdük.
            var productsDto = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(201, productsDto)); // 201 Created durum kodu
        }

        //Update

        [HttpPut]

        public async Task<IActionResult> Update(ProductUpdateDTO productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto)); //Update methodu geriye değer döndürmüyor.

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

        
        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

    }
}
