using System;
using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Mappings;

public interface IGalaxusDocumentMapper
{
    UnifiedGalaxusDocument MapToUnified(object dto, DocumentType type);
    T MapFromUnified<T>(UnifiedGalaxusDocument unified) where T : class, new();
    object MapFromUnified(UnifiedGalaxusDocument unified, DocumentType targetType);
}

// Minimal implementation to satisfy compile and allow serialization
public class GalaxusDocumentMapper : IGalaxusDocumentMapper
{
    public UnifiedGalaxusDocument MapToUnified(object dto, DocumentType type)
    {
        return new UnifiedGalaxusDocument
        {
            DocumentType = type,
            Version = "2.1",
            Type = "standard"
        };
    }

    public T MapFromUnified<T>(UnifiedGalaxusDocument unified) where T : class, new()
    {
        return new T();
    }

    public object MapFromUnified(UnifiedGalaxusDocument unified, DocumentType targetType)
    {
        // For now, just return the unified model; callers can handle or extend as needed
        return unified;
    }
}
