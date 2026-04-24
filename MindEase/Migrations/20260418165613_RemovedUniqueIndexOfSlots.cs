using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUniqueIndexOfSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots",
                columns: new[] { "DoctorWeeklyScheduleId", "StartTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots",
                columns: new[] { "DoctorWeeklyScheduleId", "StartTime" },
                unique: true);
        }
    }
}
