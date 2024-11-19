namespace Log.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int ActionTypeId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? OldData { get; set; }
        public string? NewData { get; set; }
    }

}
