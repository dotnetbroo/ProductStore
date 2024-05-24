using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Domain.Configurations;
using ProductStore.Domain.Entities.Reports;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Extensions;
using ProductStore.Service.DTOs.Reports;
using ProductStore.Service.Interfaces.Reports;

namespace ProductStore.Service.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IRepository<Report> reportRepository, IMapper mapper)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }

        public async Task<ReportForResultDto> CreateAsync(ReportForCreationDto dto)
        {
            var report = _mapper.Map<Report>(dto);
            await _reportRepository.InsertAsync(report);
            return _mapper.Map<ReportForResultDto>(report);
        }

        public async Task<ReportForResultDto> ModifyAsync(long id, ReportForUpdateDto dto)
        {
            var report = await _reportRepository.SelectAll()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
            if (report is null)
                throw new CustomException(404, "Report not found");

            _mapper.Map(dto, report);
            report.UpdatedAt = DateTime.UtcNow;

            await _reportRepository.UpdateAsync(report);

            return _mapper.Map<ReportForResultDto>(report);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var report = await _reportRepository.SelectAll()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            if (report is null)
                throw new CustomException(404, "Report not found");

            await _reportRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var reports = await _reportRepository.SelectAll()
                           .AsNoTracking()
                           .ToPagedList(@params)
                           .ToListAsync();

            return _mapper.Map<IEnumerable<ReportForResultDto>>(reports);
        }

        public async Task<ReportForResultDto> RetrieveByIdAsync(long id)
        {
            var report = await _reportRepository.SelectAll()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (report == null)
                throw new CustomException(404, "Report not found");

            return _mapper.Map<ReportForResultDto>(report);
        }
    }
}
