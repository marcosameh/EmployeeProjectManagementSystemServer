using App.Shared.Models;
using AutoMapper;
using Log.BL.DTOs;
using Log.BL.IServices;
using Log.DAL.IRepository;
using Log.Domain.Entities;
using Log.Domain.Enums;

namespace Log.BL.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper mapper;

        public AuditLogService(IAuditLogRepository auditLogRepository,IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            this.mapper = mapper;
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

        public async Task<ApiResponse<IEnumerable<AuditLogWithEmployeeDto>>> GetAuditLogs()
        {
            try
            {
                var logs = await _auditLogRepository.GetAllAsync();
                var logsResult = mapper.Map<IEnumerable<AuditLogWithEmployeeDto>>(logs);

                return new ApiResponse<IEnumerable<AuditLogWithEmployeeDto>>(true,new List<string> (),logsResult);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<AuditLogWithEmployeeDto>>(true, new List<string> { ex.Message}, null);
            }
        }
    }
}
