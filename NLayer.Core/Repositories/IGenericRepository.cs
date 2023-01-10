using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class //base repository olduğu için istediğimiz entitye uyarlamamızı sağlayacak.
    {
        Task<T> GetByIdAsync(int id);
        //IQueryable döndüğümüzde yazmış olduğumuz sorgular direk veri tabanına gitmez.Mutlaka ToList , ToListAsync gibi metodları çağırırsak o zaman veri tabanına gider.Where metodu için kullandık.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        //Get all metodu. Order by vesaire kullanıp ondan sonra ToList ile veri tabanına atabileceğimiz için Iquaryable olarak yazdık.
        IQueryable<T> GetAll();

        //Any metodu
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        //Ekle Metodu
        Task AddAsync(T entity);
        //Birden fazla ekleme
        Task AddRangeAsync(IEnumerable<T> entities);
        //Update ve Remove metodlarının EFCore tarafında asenkron metodları yoktur.
        void Update(T entity);

        void Remove(T entity);
        //Birden fazla silme işlemi
        void RemoveRange(IEnumerable<T> entities);
    }
}
