namespace GalaxusIntegration.Shared.Enum;

public enum DocumentType
{
    // Incoming
    ORDER,
    RETURNREGISTRATION,
    DISPATCHNOTIFICATION,

    // Outgoing (subset for now)
    ORDERRESPONSE,
    ORDERCHANGE,
    INVOICE,
    SHIPPINGNOTICE,
    RECEIPTACKNOWLEDGEMENT,
    DELIVERYCONFIRMATION,
    CREDITNOTE,

    // Additional outgoing/incoming types
    CANCELREQUEST,
    CANCELCONFIRMATION,
    SUPPLIERCANCELNOTIFICATION,
    SUPPLIERRETURNNOTIFICATION
}

public enum DocumentDirection
{
    Incoming,
    Outgoing
}

public enum ProcessingStatus
{
    Received,
    Validated,
    Processing,
    Processed,
    Failed,
    Queued,
    Sent,
    Acknowledged
}
