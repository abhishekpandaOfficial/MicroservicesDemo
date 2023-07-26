using Customers.API;
using Customers.API.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Title",
        Version = "v1",
        Description = "This is Microservice Demo API Services ",
        TermsOfService= new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Abhishek Panda",
            Email="Official.abhishekpanda@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/abhishekpandaofficial/")

        }

    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<CustomerDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseStaticFiles();


app.UseAuthorization();

app.MapControllers();

app.Run();
