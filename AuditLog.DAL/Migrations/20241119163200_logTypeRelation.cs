using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Log.DAL.Migrations
{
    /// <inheritdoc />
    public partial class logTypeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActionTypeId",
                table: "AuditLogs",
                column: "ActionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_ActionTypes_ActionTypeId",
                table: "AuditLogs",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_ActionTypes_ActionTypeId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_ActionTypeId",
                table: "AuditLogs");
        }
    }
}
