using NLayer.Core.Services;
using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //IQueryable döndüğümüzde yazmış olduğumuz sorgular direk veri tabanına gitmez.Mutlaka ToList , ToListAsync gibi metodları çağırırsak o zaman veri tabanına gider.Where metodu için kullandık.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync();

        //Any metodu
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        //Ekle Metodu
        Task AddAsync(T entity);
        //Birden fazla ekleme
        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);
        //Birden fazla silme işlemi
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}

//GenericRepository: db'den data alma ile ilgili işlemi gerçekleştirmektedir.

//IService ise :  GenericRepo 'dan alınan datayı işleyip controller'lara istediği datayı hazırlamak içindir. Business kod service içerisinde yazılır. try-catch yazacaksak service'ler içerisinde yazılır. DTO dönüşümleri gerçekleştireceksek yine service sınıflarında bu işlem yapılabilir.