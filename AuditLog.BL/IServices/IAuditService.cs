using App.Shared.Models;
using Log.BL.DTOs;


namespace Log.BL.IServices
{
    public interface IAuditLogService
    {
        Task LogAddition(int employeeId, string oldData, string newData);
        Task LogUpdate(int employeeId, string oldData, string newData);
        Task LogDeletion(int employeeId, string oldData, string newData);
        Task<ApiResponse<IEnumerable<AuditLogWithEmployeeDto>>> GetAuditLogs();

    }
}

