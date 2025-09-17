using GalaxusIntegration.Application.Factories;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.Mappings;
using GalaxusIntegration.Application.Services;
using GalaxusIntegration.Application.Services.Processors;
using GalaxusIntegration.Application.Strategy_Builder;
using GalaxusIntegration.Infrastructure.Database;
using GalaxusIntegration.Infrastructure.Excel_files;
using GalaxusIntegration.Infrastructure.Xml.Builders;
using GalaxusIntegration.Infrastructure.Xml.Configuration;
using GalaxusIntegration.Infrastructure.Xml.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddXmlSerializerFormatters() // Add XML support
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();

// File services
builder.Services.AddScoped<IFileGenerationService>(provider =>
    new ExcelExporter(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")));
builder.Services.AddScoped(typeof(ProductFileService));
builder.Services.AddScoped(typeof(OrderFilesServices));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("app.db"));

// XML integration services
builder.Services.AddSingleton<XmlNamespaceConfig>();
builder.Services.AddSingleton<DocumentTypeRegistry>();
builder.Services.AddScoped<IXmlParser, GenericXmlParser>();
builder.Services.AddScoped<IXmlBuilder, NamespaceAwareXmlBuilder>();

// Document processors
builder.Services.AddScoped<IDocumentProcessor, OrderProcessor>();
builder.Services.AddScoped<IDocumentProcessor, OrderResponseProcessor>();
builder.Services.AddScoped<IDocumentProcessor, DispatchNotificationProcessor>();
builder.Services.AddScoped<IDocumentProcessor, InvoiceProcessor>();
builder.Services.AddScoped<IDocumentProcessor, ReturnRegistrationProcessor>();
builder.Services.AddScoped<IDocumentProcessor, CancelRequestProcessor>();
builder.Services.AddScoped<IDocumentProcessor, SupplierCancelNotificationProcessor>();
builder.Services.AddScoped<IDocumentProcessor, SupplierReturnNotificationProcessor>();
builder.Services.AddScoped<IDocumentProcessor, CancelConfirmationProcessor>();

builder.Services.AddScoped<IDocumentProcessorFactory, DocumentProcessorFactory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderResponseService, OrderResponseService>();
builder.Services.AddScoped<ICancelRequestService, CancelRequestService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IReturnService, ReturnService>();
builder.Services.AddScoped<ICancelConfirmationService, CancelConfirmationService>();
builder.Services.AddScoped<ISupplierCancelService, SupplierCancelService>();
builder.Services.AddScoped<ISupplierReturnService, SupplierReturnService>();

// Mapping services (scan Application + API assemblies for profiles)
builder.Services.AddScoped<IGalaxusDocumentMapper, GalaxusDocumentMapper>();

// HTTP client for external API
builder.Services.AddHttpClient("GalaxusAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["GalaxusAPI:BaseUrl"] ?? "https://api.galaxus.ch");
    client.DefaultRequestHeaders.Add("Accept", "application/xml");
});
builder.Services.AddScoped<EntityBuilderStrategy>();
// Legacy services (keep for backward compatibility)
builder.Services.AddScoped<IDocumentProcessorFactory, DocumentProcessorFactory>();
builder.Services.AddScoped<OrderProcessor>();
// builder.Services.AddScoped<ReturnProcessor>(); // Commented out - ReturnProcessor not implemented yet
builder.Services.AddScoped<IXmlParserService, XmlParserService>();
builder.Services.AddScoped<IUnifiedDocumentProcessor, UnifiedOrderProcessor>();
builder.Services.AddScoped<IUnifiedDocumentProcessor, UnifiedReturnProcessor>();
builder.Services.AddScoped<IUnifiedDocumentProcessorFactory, UnifiedDocumentProcessorFactory>();


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

    // Avoid schema ID collisions for types with identical names
    c.CustomSchemaIds(type => type.FullName?.Replace('+', '.') ?? type.Name);

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
/*



*/
