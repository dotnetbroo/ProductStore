using ProductStore.Domain.Commons;
using ProductStore.Domain.Enums;

namespace ProductStore.Domain.Entities.Reports;

public class Report : Auditable
{
    public ReportType ReportType { get; set; }
    public string Reason { get; set; }
}
