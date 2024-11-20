using App.DAL.Context;
using Log.DAL.IRepository;
using Log.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Log.DAL.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AuditDbContext _context;

        public AuditLogRepository(AuditDbContext context)
        {
            _context = context;
        }

       
        public async Task<AuditLog> AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
            return auditLog;
        }

        
        public async Task<IEnumerable<AuditLogWithEmployee>> GetAllAsync()
        {
            return await _context.AuditLogWithEmployees.ToListAsync();
        }
    }
}
