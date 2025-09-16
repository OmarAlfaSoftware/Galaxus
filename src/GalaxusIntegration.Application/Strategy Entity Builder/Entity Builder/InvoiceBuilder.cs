using GalaxusIntegration.Application.DTOs.Internal;
using GalaxusIntegration.Application.DTOs.Outgoing;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Core.Entities;

namespace GalaxusIntegration.Application.Strategy_Builder.Entity_Builder
{

    public class InvoiceBuilder : IEntityBuilder
    {
        public async Task<object> Build(UnifiedDocumentDTO document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var invoice = new Invoice();

            var header = document.Header;
            var info = header?.Info;

            // Basic info
            invoice.InvoiceId = info?.InfoId ?? GenerateInvoiceId();
            invoice.InvoiceDate = info?.DocumentDate ?? DateTime.UtcNow;
            invoice.GenerationDate = header?.ControlInfo?.GenerationDate ?? DateTime.UtcNow;
            invoice.DeliveryNoteId = info?.DispatchNotificationId;
            invoice.Currency = info?.Currency ?? "CHF";

            // Order history
            if (info?.OrderHistory != null)
            {
                invoice.OrderId = info.OrderId;
                invoice.SupplierOrderId = info.OrderHistory.SupplierOrderId;
            }

            // Delivery dates
            if (info?.DeliveryDate != null)
            {
                invoice.DeliveryStartDate = info.DeliveryDate.StartDate;
                invoice.DeliveryEndDate = info.DeliveryDate.EndDate;
            }

            // Parties
            if (info?.Parties?.PartyList != null)
            {
                invoice.Parties = new();
                foreach (var p in info.Parties.PartyList)
                {
                    if (p == null) continue;

                    var party = MapParty(p);
                    invoice.Parties.Add(party);

                    // Extract invoice issuer VAT ID
                    if (p.PartyRole?.ToLower() == "invoice_issuer" && p.Address?.VatId != null)
                    {
                        invoice.VatId = p.Address.VatId;
                    }
                }
            }

            // Remarks (QR codes for Swiss invoices)
            if (info?.Remarks != null)
            {
                foreach (var remark in info.Remarks)
                {
                    if (remark.Value == "qrr")
                        invoice.QrrReference = remark.Value;
                    else if (remark.Value == "qriban")
                        invoice.QrIban = remark.Value;
                    else if (remark.Value == "scor")
                        invoice.ScorReference = remark.Value;
                }
            }

            // Invoice items
            invoice.InvoiceItems =new();
            if (document.ItemList?.Items != null)
            {
                foreach (var item in document.ItemList.Items)
                {
                    if (item == null) continue;

                    var invoiceItem = new Core.Entities.InvoiceItem
                    {
                        ProductId = item.ProductId?.SupplierPid?.Value,
                        InternationalId = item.ProductId?.InternationalPid?.Value,
                        BuyerId = item.ProductId?.BuyerPid?.Value,
                        Quantity = item.Quantity ?? 0m,
                        PriceAmount = item.ProductPriceFix?.Amount ?? 0m,
                        TaxAmount = item.ProductPriceFix?.TaxDetailsFix?. TaxAmount?? 0,
                        TaxRate = item.ProductPriceFix?.TaxDetailsFix?.TaxPercent ?? 0,
                        PriceLineAmount = item.PriceLineAmount ?? 0m,
                        OrderId = item?.OrderId,
                        DeliveryNoteId = item.DeliveryNoteId
                    };

                    invoice.InvoiceItems.Add(invoiceItem);
                }
            }

            // Summary
            if (document.Summary != null)
            {
                invoice.NetValueGoods = document.Summary?.TotalItemNum ?? 0;
                invoice.TotalAmount = document.Summary.TotalAmount ?? 0m;

                // Allow or charges (shipping costs, etc.)
                if (document.Summary.AllowOrChargeFix != null)
                {
                    invoice.AllowOrCharges =document.Summary.AllowOrChargeFix.AllowOrChargeItems.Select(z=>new AllowOrCharges{Type=z.Type,Amount=z.Amount,ChargeType=z.Name}).ToList();
                }

                // Total tax
                if (document.Summary.TotalTax != null)
                {
                    invoice.TotalTax = new Core.Entities.TotalTax
                    {
                        TaxRate = document.Summary.TotalTax.TaxRate ?? 0m,
                        TaxAmount = document.Summary.TotalTax.TaxAmount ?? 0m
                    };
                }
            }

            return invoice;
        }

        private string GenerateInvoiceId()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..10].ToUpper()}";
        }

        private Core.Entities.Party MapParty(DocumentParty p)
        {
            var addr = p.Address;
            return new Core.Entities.Party
            {
                PartyRole = p.PartyRole,
                PartyHeaders = p.PartyIds.Select(z=>new PartyHeader() { PartyType=z.Type,PartyValue=z.Value})?.ToList(),
                
                PartyData = new PartyData
                {
                    Name = addr?.Name,
                    Name2 = addr?.Name2,
                    Name3 = addr?.Name3,
                    Department = addr?.Department,
                    Title = addr?.ContactDetails?.Title,
                    FirstName = addr?.ContactDetails?.FirstName,
                    ContactName = addr?.ContactDetails?.ContactName,
                    Street = addr?.Street,
                    Zip = addr?.Zip,
                    BoxNo = addr?.BoxNo,
                    City = addr?.City,
                    CountryCode = addr?.CountryCoded,
                    Country = addr?.Country,
                    Email = addr?.Email,
                    Phone = addr?.Phone,
                    VatId = addr?.VatId
                }
            };
        }
    }
}