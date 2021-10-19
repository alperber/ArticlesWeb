using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ArticlesWeb.MVC.Models.DbEntities;

namespace ArticlesWeb.MVC.DataAccess
{
    public class ArticlesContext : IdentityUserContext<User, Guid>
    {
        public ArticlesContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
