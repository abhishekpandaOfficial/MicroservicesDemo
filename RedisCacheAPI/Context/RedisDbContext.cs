using Microsoft.EntityFrameworkCore;
using RedisCacheAPI.Models;

namespace RedisCacheAPI.Context
{
    public class RedisDbContext:DbContext
    {
        public RedisDbContext(DbContextOptions<RedisDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<ArticleMatrix> ArticleMatrices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
