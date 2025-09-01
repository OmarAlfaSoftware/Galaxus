using System.Collections.Generic;

namespace GalaxusIntegration.Shared.Constants;

public static class XmlNamespaces
{
    public const string OpenTrans = "http://www.opentrans.org/XMLSchema/2.1";
    public const string BMECat = "http://www.bmecat.org/bmecat/2005";
    public const string XmlSchema = "http://www.w3.org/2001/XMLSchema";
    public const string XmlSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";

    public static readonly Dictionary<string, string> NamespacePrefixes = new()
    {
        { OpenTrans, string.Empty },
        { BMECat, "bmecat" },
        { XmlSchema, "xsd" },
        { XmlSchemaInstance, "xsi" }
    };
}
