using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder
{

    public class InvoiceBuilder : IEntityBuilder
    {
        public async Task<object> Build(UnifiedDocumentDto document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var invoice = new Invoice();

            var header = document.Header;
            var info = header?.Metadata;

            // Basic info
            invoice.InvoiceId = info?.InvoiceId ?? GenerateInvoiceId();
            invoice.InvoiceDate = info?.DocumentDate ?? DateTime.UtcNow;
            invoice.GenerationDate = header?.ControlInfo?.GenerationDate ?? DateTime.UtcNow;
            invoice.DeliveryNoteId = info?.DeliveryNoteId;
            invoice.Currency = info?.Currency ?? "CHF";
            invoice.Parties = new();

            // Order history
            invoice.HistoryItems = info.OrderHistory.Select(z => new InvoiceHistoryItem() { OrderId = z.OrderId, SupplierOrderId = z.SupplierOrderId }).ToList();
            // Delivery dates
            if (info?.DeliveryDateRange != null)
            {
                invoice.DeliveryStartDate = info.DeliveryDateRange.EarliestDate;
                invoice.DeliveryEndDate = info.DeliveryDateRange.LatestDate;
            }

            // Parties
            foreach (DTOs.Internal.Parties party in info?.Parties)
            {
                var invoiceParty = new Core.Entities.Party();
                invoiceParty.PartyRole = party.Role;
                invoiceParty.PartyHeaders = party.PartyList.Select(z => new PartyHeader() { PartyValue = z.PartyIdValue, PartyType = z.PartyIdType }).ToList();
                var address = party.Address;
                invoiceParty.PartyData = new()
                {
                    Name = address.Name,
                    Name2 = address.NameLine2,
                    Name3 = address.NameLine3,
                    BoxNo = address.PoBoxNumber,
                    City = address.City,
                    ContactName = address.Contact.LastName,
                    Country = address.Country,
                    CountryCode = address.CountryCode,
                    Department = address.Department,
                    Email = address.EmailAddress,
                    FirstName = address.Contact.FirstName,
                    Phone = address.PhoneNumber,
                    Street = address.Street,
                    Title = address.Contact.Title,
                    VatId = address.VatIdentificationNumber,
                    Zip = address.PostalCode,
                };
                invoice.Parties.Add(invoiceParty);

            }
            // Remarks (QR codes for Swiss invoices)
            if (info?.Remarks != null)
            {
                foreach (var remark in info.Remarks)
                {
                    if (remark.Type == "qrr")
                        invoice.QrrReference = remark.Text;
                    else if (remark.Type == "qriban")
                        invoice.QrIban = remark.Text;
                    else if (remark.Type == "scor")
                        invoice.ScorReference = remark.Text;
                }
            }

            // Invoice items
            invoice.InvoiceItems = new();
            if (document.ItemList?.Items != null)
            {
                foreach (var item in document.ItemList.Items)
                {
                    if (item == null) continue;

                    var invoiceItem = new Core.Entities.InvoiceItem
                    {
                        ProductId = item.ProductDetails?.SupplierProductId?.Value,
                        InternationalId = item.ProductDetails?.InternationalProductId?.Value,
                        BuyerId = item.ProductDetails?.BuyerProductId?.Value,
                        Quantity = item.Quantity ?? 0m,
                        PriceAmount = item.LineItemPrice?.Amount ?? 0m,
                        TaxAmount = (double)(item.LineItemPrice?.TaxDetails?.Amount ?? 0),
                        TaxRate = (double)(item.LineItemPrice?.TaxDetails?.Rate ?? 0),
                        PriceLineAmount = item.LineTotalAmount ?? 0m,
                        OrderId = item?.ReferencedOrderId,
                        DeliveryNoteId = item.DeliveryReference.DeliveryNoteId,
                        StartDate = item?.ItemDeliveryDateRange?.EarliestDate,
                        EndDate = item.ItemDeliveryDateRange.LatestDate,

                    };

                    invoice.InvoiceItems.Add(invoiceItem);
                }
            }

            // Summary
            if (document.Summary != null)
            {
                invoice.NetValueGoods = (double?)(document.Summary?.TotalNetAmount ?? 0);
                invoice.TotalAmount = document.Summary.TotalGrossAmount ?? 0m;

                // Allow or charges (shipping costs, etc.)
                if (document.Summary.AllowancesAndCharges != null)
                {
                    invoice.AllowOrCharges = document.Summary.AllowancesAndCharges.Items.Select(z => new AllowOrCharges { Type = z.Type, Amount = (double)z.Amount, ChargeType = z.Name }).ToList();
                }

                // Total tax
                if (document.Summary.TotalTaxSummary != null)
                {
                    invoice.TotalTax = document.Summary.TotalTaxSummary.TaxDetailsList.Select(y => new Core.Entities.TotalTax
                    {
                        TaxRate = y.Rate ?? 0m,
                        TaxAmount = y.Amount ?? 0m
                    }).FirstOrDefault();
                }
            }

            return invoice;
        }

        private string GenerateInvoiceId()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..10].ToUpper()}";
        }

    }
}