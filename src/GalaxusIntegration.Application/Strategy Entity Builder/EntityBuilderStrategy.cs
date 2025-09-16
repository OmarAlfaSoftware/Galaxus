using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Strategy_Builder
{
    public class EntityBuilderStrategy
    {
        private readonly Dictionary<DocumentType, IEntityBuilder> _builders;
        public EntityBuilderStrategy()
        {
            _builders = new Dictionary<DocumentType, IEntityBuilder>
        {
            { DocumentType.ORDER, new OrderBuilder() },
            { DocumentType.CANCEL_REQUEST, new CancelRequestBuilder() },
            { DocumentType.RETURN_REGISTRATION, new ReturnRegistrationBuilder() },
            { DocumentType.ORDER_RESPONSE, new OrderResponseBuilder() },
            { DocumentType.DISPATCH_NOTIFICATION, new DispatchNotificationBuilder() },
            { DocumentType.INVOICE, new InvoiceBuilder() },
          //  { DocumentType.CANCEL_CONFIRMATION, new CancelConfirmationBuilder() },
          //  { DocumentType.SUPPLIER_CANCEL_NOTIFICATION, new SupplierCancelNotificationBuilder() },
          //  { DocumentType.SUPPLIER_RETURN_NOTIFICATION, new SupplierReturnNotificationBuilder() }
        };
        }
        public IEntityBuilder GetStrategy(DocumentType type)
        {
            if (_builders.TryGetValue(type, out var builder))
            {
                return builder;
            }

            throw new NotSupportedException($"No builder found for document type: {type}");
        }
    }
}
