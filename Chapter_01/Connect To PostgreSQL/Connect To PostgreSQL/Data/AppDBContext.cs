using Connect_To_PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connect_To_PostgreSQL.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
    }
}
