using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.Services;
using GalaxusIntegration.Infrastructure.Excel_files;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFileGenerationService>(provider =>
    new ExcelExporter(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")));;
builder.Services.AddScoped(typeof(ProductFileService));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Galaxus Integration API",
        Version = "v1",
        Description = "API for Galaxus Integration services"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Galaxus Integration API v1");
    });
}
//add wwwroot folder to serve static files
//publish wwwroot folder with handling the path to be in the same level as the executable





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
