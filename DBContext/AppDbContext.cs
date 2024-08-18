using Microsoft.EntityFrameworkCore;
using ToDoAPI.Model;

namespace ToDoAPI.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<AddToDoModel> ToDoDetails { get; set; }
    }
}