using System.Collections.Generic;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Shared.Constants;

public static class DocumentTypeConstants
{
    public static readonly Dictionary<DocumentType, DocumentTypeInfo> DocumentTypeInfoMap = new()
    {
        [DocumentType.ORDER] = new DocumentTypeInfo
        {
            RootElement = "ORDER",
            Direction = DocumentDirection.Incoming,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat },
            RequiredFields = new[] { "ORDER_ID", "ORDER_DATE", "PARTIES" }
        },
        [DocumentType.RETURNREGISTRATION] = new DocumentTypeInfo
        {
            RootElement = "RETURNREGISTRATION",
            Direction = DocumentDirection.Incoming,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat },
            RequiredFields = new[] { "ORDER_ID", "RETURNREGISTRATION_ID", "SHIPMENT_ID" }
        },
        [DocumentType.DISPATCHNOTIFICATION] = new DocumentTypeInfo
        {
            RootElement = "DISPATCHNOTIFICATION",
            Direction = DocumentDirection.Incoming,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat },
            RequiredFields = new[] { "DISPATCHNOTIFICATION_ID", "SHIPMENT_ID" }
        },
        [DocumentType.ORDERRESPONSE] = new DocumentTypeInfo
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
        [DocumentType.CANCELREQUEST] = new DocumentTypeInfo
        {
            RootElement = "CANCELREQUEST",
            Direction = DocumentDirection.Outgoing,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat }
        },
        [DocumentType.CANCELCONFIRMATION] = new DocumentTypeInfo
        {
            RootElement = "CANCELCONFIRMATION",
            Direction = DocumentDirection.Outgoing,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat }
        },
        [DocumentType.SUPPLIERCANCELNOTIFICATION] = new DocumentTypeInfo
        {
            RootElement = "SUPPLIERCANCELNOTIFICATION",
            Direction = DocumentDirection.Outgoing,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat }
        },
        [DocumentType.SUPPLIERRETURNNOTIFICATION] = new DocumentTypeInfo
        {
            RootElement = "SUPPLIERRETURNNOTIFICATION",
            Direction = DocumentDirection.Outgoing,
            RequiredNamespaces = new[] { XmlNamespaces.BMECat },
            RequiredFields = new[] { "REQUESTACCEPTED" }
        }
    };
}

public class DocumentTypeInfo
{
    public string RootElement { get; set; } = string.Empty;
    public DocumentDirection Direction { get; set; } = DocumentDirection.Incoming;
    public string[]? RequiredNamespaces { get; set; }
    public string[]? RequiredFields { get; set; }

    public string HeaderElement => $"{RootElement}_HEADER";
    public string InfoElement => $"{RootElement}_INFO";
    public string ItemListElement => $"{RootElement}_ITEM_LIST";
    public string ItemElement => $"{RootElement}_ITEM";
    public string SummaryElement => $"{RootElement}_SUMMARY";
}
