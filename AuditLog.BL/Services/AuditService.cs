using Log.BL.IServices;
using Log.DAL.IRepository;
using Log.Domain.Entities;
using Log.Domain.Enums;

namespace Log.BL.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task LogAddition(int employeeId, string oldData, string newData)
        {
            await AddAuditLog(employeeId, ActionTypeEnum.Add, oldData, newData);
        }

        public async Task LogUpdate(int employeeId, string oldData, string newData)
        {
            await AddAuditLog(employeeId, ActionTypeEnum.Update, oldData, newData);
        }

        public async Task LogDeletion(int employeeId, string oldData, string newData)
        {
            await AddAuditLog(employeeId, ActionTypeEnum.Delete, oldData, newData);
        }

        private async Task AddAuditLog(int employeeId, ActionTypeEnum actionType, string oldData, string newData)
        {
            var auditLog = new AuditLog
            {
                EmployeeId = employeeId,
                ActionTypeId = (int)actionType,
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
