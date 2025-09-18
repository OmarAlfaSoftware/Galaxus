using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Infrastructure.Xml.Parsers;

public interface IXmlParser
{
    UnifiedDocumentDto Parse(string xmlContent);
    T Parse<T>(string xmlContent) where T : class;
    DocumentType IdentifyDocumentType(string xmlContent);
}

