using GalaxusIntegration.Application.Factories;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.Services;
using GalaxusIntegration.Infrastructure.Excel_files;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddXmlSerializerFormatters() // Add XML support
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFileGenerationService>(provider =>
    new ExcelExporter(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")));;
builder.Services.AddScoped(typeof(ProductFileService));
builder.Services.AddScoped(typeof(OrderFilesServices));
builder.Services.AddScoped<IDocumentProcessorFactory, DocumentProcessorFactory>();
builder.Services.AddScoped<OrderProcessor>();
builder.Services.AddScoped<ReturnProcessor>();
builder.Services.AddScoped<IXmlParserService, XmlParserService>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Galaxus Integration API", Version = "v1" });

    // Add XML comments if you have them
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configure Swagger to handle XML properly
    c.UseInlineDefinitionsForEnums();
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
