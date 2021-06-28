using ArticlesWeb.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.Repository
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
