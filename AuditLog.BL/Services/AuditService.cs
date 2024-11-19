using Log.BL.IServices;
using Log.DAL.IRepository;
using Log.Domain.Entities;

namespace Log.BL.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task AddAuditLog(int employeeId, int actionTypeId, string oldData, string newData)
        {
            var auditLog = new AuditLog
            {
                EmployeeId = employeeId,
                ActionTypeId = actionTypeId,
                Timestamp = DateTime.UtcNow,
                OldData = oldData,
                NewData = newData
            };

            await _auditLogRepository.AddAsync(auditLog);
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogs()
        {
            return await _auditLogRepository.GetAllAsync();
        }
    }

}
