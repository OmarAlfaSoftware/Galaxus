using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Infrastructure.Xml.Builders;

public interface IXmlBuilder
{
    string Build<T>(T dto, DocumentType documentType) where T : class;
    string BuildWithNamespaces<T>(T dto, NamespaceConfiguration config) where T : class;
}

public class NamespaceConfiguration
{
    public string DefaultNamespace { get; set; }
    public Dictionary<string, string> Namespaces { get; set; }
    public Dictionary<string, string> ElementNamespaces { get; set; }
}
