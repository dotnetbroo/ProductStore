using ProductStore.Domain.Enums;

namespace ProductStore.Service.DTOs.Reports;

public record ReportForCreationDto
{
    public ReportType ReportType { get; set; }
    public string Reason { get; set; }
}
