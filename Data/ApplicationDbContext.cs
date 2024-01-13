using Microsoft.EntityFrameworkCore;
using newAspProject.Models;

namespace WorldDominion.Models
{
    public class ApplicationDbContext : DbContext
    {
        //this creates the db context & used for migrations
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //define modal name and table name
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> products { get; set; }
    }
}