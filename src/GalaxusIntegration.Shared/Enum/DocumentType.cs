namespace GalaxusIntegration.Shared.Enum;

public enum DocumentType
{
    // Incoming
    ORDER,
    RETURN_REGISTRATION,
    DISPATCH_NOTIFICATION,

    // Outgoing (subset for now)
    ORDER_RESPONSE,
    ORDER_CHANGE,
    INVOICE,
    SHIPPING_NOTICE,
    RECEIPT_ACKNOWLEDGEMENT,
    DELIVERY_CONFIRMATION,
    CREDIT_NOTE,

    // Additional outgoing/incoming types
    CANCEL_REQUEST,
    CANCEL_CONFIRMATION,
    SUPPLIER_CANCEL_NOTIFICATION,
    SUPPLIER_RETURN_NOTIFICATION
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
