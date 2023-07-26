using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



/* Database Dependency Injection */

var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
var dbport = 3306;
var dbname = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
var dbuser = "root";

var connectionstring = $"server={dbhost};port={dbport};database={dbname};user={dbuser};password={dbPassword}";

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseMySQL(connectionstring);
});


/* =================================== */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
