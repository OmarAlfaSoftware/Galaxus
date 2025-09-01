using System.Xml;
using System.Xml.Serialization;

namespace GalaxusIntegration.Application.XML;

public class XMLBuilder
{
    private const string Version = "2.1";
    private const string StandardType = "standard";
    private const string Namespace = "http://www.opentrans.org/XMLSchema/2.1";
    private readonly string Header;

    public XMLBuilder(string header)
    {
        Header = header;
    }
    public string BuildXML<T>(T XMLNode) where T : class
    {
        XmlRootAttribute xmlRoot = new XmlRootAttribute
        {
            ElementName = Header,
            Namespace = Namespace,
            IsNullable = false
        };

        var xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

        using var stringWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = false
        });

        xmlSerializer.Serialize(xmlWriter, XMLNode);
        xmlWriter.Flush();

        // Load the XML to add attributes
        var doc = new XmlDocument();
        doc.LoadXml(stringWriter.ToString());

        // Add attributes to the root element
        var root = doc.DocumentElement;
        root.SetAttribute("type", StandardType);
        root.SetAttribute("version", Version);
        return doc.OuterXml;

    }
}