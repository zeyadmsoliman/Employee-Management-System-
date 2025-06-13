using EMS.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMS.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly AppDBContext db;
        protected DbSet<T> dbSet;
        public Repository(AppDBContext db)
        {
            dbSet =db.Set<T>();
            this.db = db;
        }
        public async Task AddAsync(T entity)
        {
            dbSet.AddAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var ent= await dbSet.FindAsync(id);
             dbSet.Remove(ent);
        }
        public async Task<T?> FindById(int id)
        {
            var ent = await dbSet.FindAsync(id);
            return ent;
        }
        public async Task <List<T>> GetAll()
        {
            var list=await dbSet.ToListAsync();
            return list;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            var list = await dbSet.AsQueryable().Where(filter).ToListAsync();
            return list;
        }

        public async Task<int> SaveChangesAsync()
        {
            return(await db.SaveChangesAsync());
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }

       
    }
}
