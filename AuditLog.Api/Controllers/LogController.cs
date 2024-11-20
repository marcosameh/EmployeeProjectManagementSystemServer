using App.Shared.Models;
using Log.BL.DTOs;
using Log.BL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Log.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public LogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }



        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AuditLogWithEmployeeDto>>>> GetAll()
        {
            var response = await _auditLogService.GetAuditLogs();
            return Ok(response);
        }

    }
}
