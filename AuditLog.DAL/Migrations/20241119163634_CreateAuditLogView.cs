using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Log.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateAuditLogView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW AuditLogWithEmployee AS
                SELECT 
                    al.Id AS AuditLogId,
                    al.EmployeeId,
                    e.Name AS EmployeeName,
                    al.ActionTypeId,
                    at.Name AS ActionTypeName,
                    al.Timestamp,
                    al.OldData,
                    al.NewData
                FROM AuditLogs al
                LEFT JOIN Employees e ON al.EmployeeId = e.Id
                LEFT JOIN ActionTypes at ON al.ActionTypeId = at.Id
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS AuditLogWithEmployee");
        }
    }
}
