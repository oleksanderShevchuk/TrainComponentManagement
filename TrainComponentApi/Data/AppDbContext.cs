using System.Collections.Generic;

namespace TrainComponentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Component> Components { get; set; }
    }
}
