using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.BL.DTOs
{
    public class AuditLogWithEmployeeDto
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
