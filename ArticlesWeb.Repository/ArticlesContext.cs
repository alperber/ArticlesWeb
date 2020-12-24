using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.Repository
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer("Server=WESTWORLD\\SQLEXPRESS;Database=WebProje1;Trusted_Connection=true");
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
