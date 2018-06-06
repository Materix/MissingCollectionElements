using Microsoft.EntityFrameworkCore;

namespace MissingCollectionElements.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Container> Containers { get; set; }

        public DbSet<Link> Links { get; set; }
    }
}