using Connect_To_PostgreSQL.Data;
using Connect_To_PostgreSQL.Dto;
using Connect_To_PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connect_To_PostgreSQL.Endpoints
{
    public static class ArticleEndpoints
    {
        public static void MyArticleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Hello World!");
            app.MapPost("articles", async (CreateArticleRequest request, AppDBContext context) =>
            {
                var article = new Article()
                {
                    Title = request.Title,
                    Description = request.Description,
                };

                await context.AddAsync(article);
                await context.SaveChangesAsync();

                return Results.Ok(article);
            });


            app.MapGet("articles", async (
                       AppDBContext context,
                       int page = 1,
                       int pageSize = 10) =>
            {
                var articles = await context.Articles
                    .AsNoTracking()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Results.Ok(articles);
            });

            app.MapGet("articles/{id}", async (
                int id,
                AppDBContext context,
                CancellationToken ct) =>
            {
                var articles = await context.Articles
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.Id == id);

                return articles is null ? Results.NotFound() : Results.Ok(articles);
            });

            app.MapPut("articles/{id}", async (
                int id,
                UpdateArticleRequest request,
                AppDBContext context) =>
            {
                var article = await context.Articles
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (article is null)
                {
                    return Results.NotFound();
                }

                article.Title = request.Title;
                article.Description = request.Description;

                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("articles/{id}", async (
                int id,
                AppDBContext context) =>
            {
                var article = await context.Articles
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (article is null)
                {
                    return Results.NotFound();
                }

                context.Remove(article);

                await context.SaveChangesAsync();

                return Results.NoContent();
            });


        }
    }
}
