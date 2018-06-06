using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MissingCollectionElements.API
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Container> Containers { get; set; }

        public DbSet<Link> Links { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
