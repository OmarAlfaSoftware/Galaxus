using GalaxusIntegration.Application.Services.Processors;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Factories;

public interface IDocumentProcessorFactory
{
    IDocumentProcessor GetProcessor(DocumentType type);
}

public class DocumentProcessorFactory : IDocumentProcessorFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<IDocumentProcessor> _processors;
    
    public DocumentProcessorFactory(
        IServiceProvider serviceProvider,
        IEnumerable<IDocumentProcessor> processors)
    {
        _serviceProvider = serviceProvider;
        _processors = processors;
    }
    
    public IDocumentProcessor GetProcessor(DocumentType type)
    {
        var processor = _processors.FirstOrDefault(p => p.CanProcess(type));
        
        if (processor == null)
        {
            throw new NotSupportedException($"No processor found for document type: {type}");
        }
        
        return processor;
    }
}