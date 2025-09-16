using System.Collections.Generic;
using System.Linq;
using GalaxusIntegration.Application.Services.Processors;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Factories;

public interface IUnifiedDocumentProcessorFactory
{
    IUnifiedDocumentProcessor GetProcessor(DocumentType type);
}

public class UnifiedDocumentProcessorFactory : IUnifiedDocumentProcessorFactory
{
    private readonly IEnumerable<IUnifiedDocumentProcessor> _processors;

    public UnifiedDocumentProcessorFactory(IEnumerable<IUnifiedDocumentProcessor> processors)
    {
        _processors = processors;
    }

    public IUnifiedDocumentProcessor GetProcessor(DocumentType type)
    {
        var proc = _processors.FirstOrDefault(p => p.CanProcess(type));
        if (proc == null) throw new NotSupportedException($"No unified processor found for {type}");
        return proc;
    }
}
