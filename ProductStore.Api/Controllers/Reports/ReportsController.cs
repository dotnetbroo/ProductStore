using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Reports;
using ProductStore.Service.Interfaces.Reports;
using System.Threading.Tasks;

namespace ProductStore.Api.Controllers.Reports
{
  //  [Authorize]
    public class ReportsController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

       // [Authorize("Admins")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertAsync([FromBody] ReportForCreationDto dto)
        {
            var result = await _reportService.CreateAsync(dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        //[Authorize("Admins")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await _reportService.RetrieveAllAsync(@params);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

       // [Authorize("Admins")]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _reportService.RetrieveByIdAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

       // [Authorize("Admins")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var result = await _reportService.RemoveAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
       // [Authorize("Admins")]
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] ReportForUpdateDto dto)
        {
            var result = await _reportService.ModifyAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
