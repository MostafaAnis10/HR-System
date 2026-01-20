using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HR_System.DAL.Database
{
    public class HrDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> Department { get; set; }


        public HrDbContext(DbContextOptions<HrDbContext> options):base(options) 
        {
            
        }
               
    }
}
