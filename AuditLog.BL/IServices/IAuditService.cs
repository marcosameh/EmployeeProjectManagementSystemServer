namespace Log.BL.IServices
{
    public interface IAuditLogService
    {
        Task AddAuditLog(int employeeId, int actionTypeId, string oldData, string newData);
    }
}
