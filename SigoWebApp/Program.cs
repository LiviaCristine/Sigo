using SigoWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("DataSource=:memory:"));

var app = builder.Build();

// Criar e manter o banco em memória ativo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.OpenConnection();        // necessário para manter o banco em memória ativo
    db.Database.EnsureCreated();         // cria as tabelas
}

