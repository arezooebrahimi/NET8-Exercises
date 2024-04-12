using Connect_To_PostgreSQL.Data;
using Connect_To_PostgreSQL.Dto;
using Connect_To_PostgreSQL.Endpoints;
using Connect_To_PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MyArticleEndpoints();

app.Run();
