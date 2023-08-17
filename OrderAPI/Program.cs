using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// ************ Adding SERILOG For LOGS *********//

var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



//ConfigureLogging();

//builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


// Elastic Search Configurations
//void ConfigureLogging()
//{
//    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//    var configuration = new ConfigurationBuilder()
//        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//        .AddJsonFile(
//            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
//            optional: true)
//        .Build();

//    Log.Logger = new LoggerConfiguration()
//        .Enrich.FromLogContext()
//        .Enrich.WithEnvironmentName()
//        .Enrich.WithMachineName()
//        .WriteTo.Debug()
//        .WriteTo.Console()
//        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
//        .Enrich.WithProperty("Environment", environment!)
//        .ReadFrom.Configuration(configuration)
//        .CreateLogger();
//}

//ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
//{
//    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
//    {
        
//        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
//        AutoRegisterTemplate = true,
//        NumberOfReplicas = 1,
//        NumberOfShards = 2
         
    
//    };
//}
