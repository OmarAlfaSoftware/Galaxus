using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GalaxusIntegration.Infrastructure.Xml.Configuration;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Shared.Enum;
using Microsoft.Extensions.Logging;

namespace GalaxusIntegration.Infrastructure.Xml.Builders;

public class NamespaceAwareXmlBuilder : IXmlBuilder
{
    private readonly XmlNamespaceConfig _namespaceConfig;
    private readonly ILogger<NamespaceAwareXmlBuilder> _logger;
    
    public NamespaceAwareXmlBuilder(
        XmlNamespaceConfig namespaceConfig,
        ILogger<NamespaceAwareXmlBuilder> logger)
    {
        _namespaceConfig = namespaceConfig;
        _logger = logger;
    }
    
    public string Build<T>(T dto, DocumentType documentType) where T : class
    {
        var sourceConfig = _namespaceConfig.Configurations[documentType];
        var config = new GalaxusIntegration.Infrastructure.Xml.Builders.NamespaceConfiguration
        {
            DefaultNamespace = sourceConfig.DefaultNamespace,
            Namespaces = sourceConfig.Namespaces,
            ElementNamespaces = sourceConfig.ElementNamespaces
        };
        return BuildWithNamespaces(dto, config);
    }
    
    public string BuildWithNamespaces<T>(T dto, GalaxusIntegration.Infrastructure.Xml.Builders.NamespaceConfiguration config) where T : class
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = false,
            Encoding = Encoding.UTF8
        };
        
        using var stringWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, settings);
        
        // Create namespace manager
        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", config.DefaultNamespace); // Default namespace
        
        foreach (var ns in config.Namespaces)
        {
            namespaces.Add(ns.Key, ns.Value);
        }
        
        // Create serializer with custom namespace handling
        var serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(xmlWriter, dto, namespaces);
        
        return stringWriter.ToString();
    }
}
