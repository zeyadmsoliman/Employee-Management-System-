using System.Linq.Expressions;

namespace EMS.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> FindById(int id);
        
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int Id);
    Task<int> SaveChangesAsync();
    }
}
