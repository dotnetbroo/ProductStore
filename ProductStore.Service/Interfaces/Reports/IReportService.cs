using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Reports;

namespace ProductStore.Service.Interfaces.Reports;

public interface IReportService
{
    Task<bool> RemoveAsync(long id);
    Task<ReportForResultDto> RetrieveByIdAsync(long id);
    Task<ReportForResultDto> CreateAsync(ReportForCreationDto dto);
    Task<ReportForResultDto> ModifyAsync(long id, ReportForUpdateDto dto);
    Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
