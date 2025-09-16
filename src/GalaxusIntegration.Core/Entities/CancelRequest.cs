using GalaxusIntegration.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxusIntegration.Core.Entities
{
    public class CancelRequest : BaseDocument
    {
        public string OrderId { get; set; }
        public DateTime CancelRequestDate { get; set; }
        public string Language { get; set; }
        public List<Party> Parties { get; set; } = new();
        public string BuyerIdRef { get; set; }
        public string SupplierIdRef { get; set; }
        public List<CancelRequestItem> ItemsToCancel { get; set; } = new();
        public int TotalItemNum { get; set; }
        public ProcessingStatus Status { get; set; }
    }
    public class CancelRequestItem
    {
        public string LineItemId { get; set; }
        public string ProductId { get; set; }
        public string InternationalId { get; set; }
        public string BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public string OrderUnit { get; set; }
    }

}
