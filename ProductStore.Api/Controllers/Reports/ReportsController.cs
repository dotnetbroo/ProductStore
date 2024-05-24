using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.OrderItems;
using ProductStore.Service.DTOs.Reports;
using ProductStore.Service.Interfaces.Reports;

namespace ProductStore.Api.Controllers.Reports;

public class ReportsController : BaseController
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromForm] ReportForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _reportService.CreateAsync(dto)
            });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await _reportService.RetrieveAllAsync(@params)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await _reportService.RetrieveByIdAsync(id)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await _reportService.RemoveAsync(id)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] ReportForUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await _reportService.ModifyAsync(id, dto)
        });
}
