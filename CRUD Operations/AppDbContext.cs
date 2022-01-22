using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<CardModel>CardTable { get; set; }
    }
}
