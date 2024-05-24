using ProductStore.Domain.Enums;

namespace ProductStore.Service.DTOs.Reports;

public record ReportForResultDto
{
    public long Id { get; set; }
    public ReportType ReportType { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
