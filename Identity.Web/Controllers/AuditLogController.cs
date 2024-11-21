using Log.BL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    [Authorize(Roles = "Auditor")]
    public class AuditLogController : Controller
    {
        private readonly IAuditLogService auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            this.auditLogService = auditLogService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var getLogsResult = await auditLogService.GetAuditLogs();
            return View(getLogsResult.Data);
        }
    }
}
