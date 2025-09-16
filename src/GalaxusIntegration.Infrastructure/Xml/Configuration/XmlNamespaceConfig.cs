using System.Collections.Generic;
using GalaxusIntegration.Infrastructure.Xml.Builders;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Infrastructure.Xml.Configuration;

public class XmlNamespaceConfig
{
    public Dictionary<DocumentType, NamespaceConfiguration> Configurations { get; set; }
    
    public XmlNamespaceConfig()
    {
        Configurations = new Dictionary<DocumentType, NamespaceConfiguration>();
        InitializeConfigurations();
    }
    
    private void InitializeConfigurations()
    {
        // Order Response configuration
        Configurations[DocumentType.ORDER_RESPONSE] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "LANGUAGE", XmlNamespaces.BMECat },
                { "CURRENCY", XmlNamespaces.BMECat },
                { "PARTY_ID", XmlNamespaces.BMECat },
                { "ORDER_UNIT", XmlNamespaces.BMECat }
            }
        };
        
        // Invoice configuration
        Configurations[DocumentType.INVOICE] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "LANGUAGE", XmlNamespaces.BMECat },
                { "CURRENCY", XmlNamespaces.BMECat },
                { "PARTY_ID", XmlNamespaces.BMECat },
                { "VAT_ID", XmlNamespaces.BMECat }
            }
        };
        
        // Order configuration
        Configurations[DocumentType.ORDER] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "LANGUAGE", XmlNamespaces.BMECat },
                { "CURRENCY", XmlNamespaces.BMECat },
                { "PARTY_ID", XmlNamespaces.BMECat },
                { "ORDER_UNIT", XmlNamespaces.BMECat }
            }
        };
        
        // Return Registration configuration
        Configurations[DocumentType.RETURN_REGISTRATION] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "LANGUAGE", XmlNamespaces.BMECat },
                { "PARTY_ID", XmlNamespaces.BMECat },
                { "ORDER_UNIT", XmlNamespaces.BMECat }
            }
        };
        
        // Dispatch Notification configuration
        Configurations[DocumentType.DISPATCH_NOTIFICATION] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "PARTY_ID", XmlNamespaces.BMECat }
            }
        };
        
        // Cancel Request configuration
        Configurations[DocumentType.CANCEL_REQUEST] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "LANGUAGE", XmlNamespaces.BMECat },
                { "PARTY_ID", XmlNamespaces.BMECat },
                { "ORDER_UNIT", XmlNamespaces.BMECat }
            }
        };
        
        // Cancel Confirmation configuration
        Configurations[DocumentType.CANCEL_CONFIRMATION] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "PARTY_ID", XmlNamespaces.BMECat }
            }
        };
        
        // Supplier Cancel Notification configuration
        Configurations[DocumentType.SUPPLIER_CANCEL_NOTIFICATION] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "PARTY_ID", XmlNamespaces.BMECat }
            }
        };
        
        // Supplier Return Notification configuration
        Configurations[DocumentType.SUPPLIER_RETURN_NOTIFICATION] = new NamespaceConfiguration
        {
            DefaultNamespace = XmlNamespaces.OpenTrans,
            Namespaces = new Dictionary<string, string>
            {
                { "bmecat", XmlNamespaces.BMECat },
                { "xsi", XmlNamespaces.XmlSchemaInstance }
            },
            ElementNamespaces = new Dictionary<string, string>
            {
                { "PARTY_ID", XmlNamespaces.BMECat }
            }
        };
    }
}

public class NamespaceConfiguration
{
    public string DefaultNamespace { get; set; } = XmlNamespaces.OpenTrans;
    public Dictionary<string, string> Namespaces { get; set; } = new();
    public Dictionary<string, string> ElementNamespaces { get; set; } = new();
}
