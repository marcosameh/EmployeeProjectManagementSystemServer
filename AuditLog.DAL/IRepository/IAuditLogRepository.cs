using Log.Domain.Entities;

namespace Log.DAL.IRepository
{
    public interface IAuditLogRepository
    {

        Task<AuditLog> AddAsync(AuditLog auditLog);
        Task<IEnumerable<AuditLog>> GetAllAsync();
    }
}
