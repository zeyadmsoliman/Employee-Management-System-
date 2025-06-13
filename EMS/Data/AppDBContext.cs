using EMS.Model;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
           : base(options)

        {

        }
   public DbSet<Employee> Employees { get; set; }
   public DbSet<Department> departments { get; set; }
   public DbSet<UserData>  users { get; set; }
    }
}
