using BasicWebAPI.Data;
using BasicWebAPI.Middlewares;
using BasicWebAPI.Repository;
using BasicWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(); 

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.WithExceptionDetails()
    .CreateLogger();

builder.Host.UseSerilog(); 

builder.Services.AddControllers(); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Basic Web API",
        Version = "v1",
        Description = "API Documentation"
    });
});

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic Web API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseSwagger(); 
app.UseSwaggerUI(); 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
