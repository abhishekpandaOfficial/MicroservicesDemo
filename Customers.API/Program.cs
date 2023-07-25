using Customers.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/*  Database Context Dependency Injection */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
var ConnectionString = $"Data Source ={dbHost};Initial Catalog={dbName};User ID=sa; Password={dbPassword};Trusted_Connection=True; Persist Security Info=True; TrustServerCertificate=true;";

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(ConnectionString));

/* ==================================== */

var app = builder.Build();

// Configure the HTTP request pipeline.

 app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
