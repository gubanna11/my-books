using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using my_books.Data;
using my_books.Data.Services;
using my_books.Exceptions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

//Log.Logger = new LoggerConfiguration()
//	.WriteTo.MSSqlServer("Data Source=COMPUTER\\MSSQLEXPRESS;Initial Catalog=my-books-db;Integrated Security=True;Encrypt=False",
//	sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", SchemaName = "dbo", AutoCreateSqlTable = true },
//	appConfiguration: configuration
//		)
//		.CreateLogger();


builder.Logging.ClearProviders();

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.Build();


var logger = new LoggerConfiguration()
	  .ReadFrom.Configuration(configuration)
				.CreateLogger();

//var logger = new LoggerConfiguration()
//	.WriteTo.MSSqlServer("Data Source=COMPUTER\\MSSQLEXPRESS;Initial Catalog=my-books-db;Integrated Security=True;Encrypt=False",
//	sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", SchemaName = "dbo", AutoCreateSqlTable = true },
//	appConfiguration: configuration
//		)
//		.CreateBootstrapLogger();

Log.Logger = logger;

builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//	c.SwaggerDoc("v2", new OpenApiInfo { Title = "my_books_updated", Version = "v2" });
//});



//Configure DBContext with SQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Configure the Services
builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<AuthorsService>();
builder.Services.AddTransient<PublishersService>();



//builder.Services.AddApiVersioning();
builder.Services.AddApiVersioning(config =>
{
	config.DefaultApiVersion = new ApiVersion(1, 0);
	config.AssumeDefaultVersionWhenUnspecified = true;

	//config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
	//config.ApiVersionReader = new MediaTypeApiVersionReader();


});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//	//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json",
//	//	"my_books_ui_updated v2"));

//}

app.UseHttpsRedirection();

app.UseAuthorization();

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
	builder.AddConsole();
	builder.AddDebug();
});
//Exception Handling
app.ConfigureBuildInExceptionHandler(loggerFactory);
//app.ConfigureCustomExceptionHandler();

app.MapControllers();

AppDbInitializer.Seed(app);

app.Run();



