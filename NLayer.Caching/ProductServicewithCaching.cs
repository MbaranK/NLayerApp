using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitofWork;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    //ÇOK SIK DEĞİŞMEYECEK AMA ÇOK SIK ERİŞECEĞİMİZ BİR DATA EN UYGUN CACHE ADAYIDIR.
    public class ProductServicewithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitofWork _unitofWork;

        public ProductServicewithCaching(IUnitofWork unitofWork, IProductRepository productRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _productRepository = productRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;

            //İlk nesne örneği oluşturulduğunda cacheleme işlemi yapmamız gerekiyor.

            //cache keyine sahip data varmı yokmu ona bakıyoruz sadece bu yüzden out dan sonra memory'de boşa yer kaplamasın diye sadece "_" tuttuk.

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetProductswithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitofWork.CommitAsync();
            await CacheallProductsAsync();
            return entity;

        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities);
            await _unitofWork.CommitAsync();
            await CacheallProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductswithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDTO>>(products);

            return Task.FromResult( CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsWithCategoryDto));
        }

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitofWork.CommitAsync();
            await CacheallProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _productRepository.RemoveRange(entities);
            await _unitofWork.CommitAsync();
            await CacheallProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitofWork.CommitAsync();
            await CacheallProductsAsync();
            
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheallProductsAsync()
        {
            //Her çağırdığımızda datayı çekip cacheleyecek.
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
