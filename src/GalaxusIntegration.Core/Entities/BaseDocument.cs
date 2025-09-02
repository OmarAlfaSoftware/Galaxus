using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Core.Entities;

public abstract class BaseDocument
{
    public Guid? Id { get; set; }
    public string? DocumentId { get; set; }
 //   public DocumentType? Type { get; set; }
    public DocumentDirection? Direction { get; set; }
    public ProcessingStatus? Status { get; set; }
    public string? RawXml { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public int? RetryCount { get; set; }
   // public Dictionary<string, object> Metadata { get; set; } = new();
}
