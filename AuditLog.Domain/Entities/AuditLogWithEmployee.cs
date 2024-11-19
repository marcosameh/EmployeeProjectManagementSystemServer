namespace Log.Domain.Entities
{
    public class AuditLogWithEmployee
    {
        public int AuditLogId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int ActionTypeId { get; set; }
        public string? ActionTypeName { get; set; }
        public DateTime Timestamp { get; set; }
        public string? OldData { get; set; }
        public string? NewData { get; set; }
    }
}
