using System.Collections.Generic;
using System.Linq;
using GalaxusIntegration.Shared.Constants;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Infrastructure.Xml.Configuration;

public class DocumentTypeRegistry
{
    private readonly Dictionary<DocumentType, DocumentTypeInfo> _configurations;
    
    public DocumentTypeRegistry()
    {
        _configurations = InitializeConfigurations();
    }
    
    private Dictionary<DocumentType, DocumentTypeInfo> InitializeConfigurations()
    {
        return new Dictionary<DocumentType, DocumentTypeInfo>
        {
            [DocumentType.ORDER] = new DocumentTypeInfo
            {
                RootElement = "ORDER",
                Direction = DocumentDirection.Incoming,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat },
                RequiredFields = new[] { "ORDER_ID", "ORDER_DATE", "PARTIES" }
            },
            [DocumentType.RETURN_REGISTRATION] = new DocumentTypeInfo
            {
                RootElement = "RETURNREGISTRATION",
                Direction = DocumentDirection.Incoming,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat },
                RequiredFields = new[] { "ORDER_ID", "RETURNREGISTRATION_ID", "SHIPMENT_ID" }
            },
            [DocumentType.DISPATCH_NOTIFICATION] = new DocumentTypeInfo
            {
                RootElement = "DISPATCHNOTIFICATION",
                Direction = DocumentDirection.Incoming,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat },
                RequiredFields = new[] { "DISPATCHNOTIFICATION_ID", "SHIPMENT_ID" }
            },
            [DocumentType.ORDER_RESPONSE] = new DocumentTypeInfo
            {
                RootElement = "ORDERRESPONSE",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat }
            },
            [DocumentType.INVOICE] = new DocumentTypeInfo
            {
                RootElement = "INVOICE",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat },
                RequiredFields = new[] { "INVOICE_ID", "INVOICE_DATE", "VAT_ID" }
            },
            [DocumentType.CANCEL_REQUEST] = new DocumentTypeInfo
            {
                RootElement = "CANCELREQUEST",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat }
            },
            [DocumentType.CANCEL_CONFIRMATION] = new DocumentTypeInfo
            {
                RootElement = "CANCELCONFIRMATION",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat }
            },
            [DocumentType.SUPPLIER_CANCEL_NOTIFICATION] = new DocumentTypeInfo
            {
                RootElement = "SUPPLIERCANCELNOTIFICATION",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat }
            },
            [DocumentType.SUPPLIER_RETURN_NOTIFICATION] = new DocumentTypeInfo
            {
                RootElement = "SUPPLIERRETURNNOTIFICATION",
                Direction = DocumentDirection.Outgoing,
                RequiredNamespaces = new[] { XmlNamespaces.BMECat },
                RequiredFields = new[] { "REQUESTACCEPTED" }
            }
        };
    }
    
    public DocumentTypeInfo GetConfiguration(DocumentType type)
    {
        return _configurations[type];
    }
    
    public DocumentType IdentifyDocumentType(string rootElement)
    {
        return _configurations
            .FirstOrDefault(x => x.Value.RootElement == rootElement)
            .Key;
    }
    
    public DocumentType GetDocumentTypeByRootElement(string rootElement)
    {
        return _configurations
            .FirstOrDefault(x => x.Value.RootElement == rootElement)
            .Key;
    }
}

public class DocumentTypeInfo
{
    public string RootElement { get; set; }
    public DocumentDirection Direction { get; set; }
    public string[] RequiredNamespaces { get; set; }
    public string[] RequiredFields { get; set; }
    
    // Dynamic property generation based on root element
    public string HeaderElement => $"{RootElement}_HEADER";
    public string InfoElement => $"{RootElement}_INFO";
    public string ItemListElement => $"{RootElement}_ITEM_LIST";
    public string ItemElement => $"{RootElement}_ITEM";
    public string SummaryElement => $"{RootElement}_SUMMARY";
}
