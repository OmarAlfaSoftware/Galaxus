using GalaxusIntegration.Application.DTOs.PartialDTOs;

namespace GalaxusIntegration.Application.DTOs.Order_Coming_Requests_DTOs;

public class ReturnOrderRequestDTO
{
    Header ReturnHeader { get; set; }
    ItemList ReturnItemList { get; set; }
    Summary ReturnSummary { get; set; }
}