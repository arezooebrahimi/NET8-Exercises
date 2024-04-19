using Connect_To_PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;

namespace Connect_To_PostgreSQL.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDBContext dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            dbContext.Database.Migrate();
        }
    }
}
