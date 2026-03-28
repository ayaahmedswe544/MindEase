using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class EditDoctorSessionSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSessionSlots_AspNetUsers_DoctorId",
                table: "DoctorSessionSlots");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSessionSlots_DoctorId_StartDateTime",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "SlotStatus",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "DoctorSessionSlots");

            migrationBuilder.AddColumn<int>(
                name: "DoctorWeeklyScheduleId",
                table: "DoctorSessionSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "DoctorSessionSlots",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "DoctorSessionSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "DoctorSessionSlots",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots",
                columns: new[] { "DoctorWeeklyScheduleId", "StartTime" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSessionSlots_DoctorWeeklySchedules_DoctorWeeklyScheduleId",
                table: "DoctorSessionSlots",
                column: "DoctorWeeklyScheduleId",
                principalTable: "DoctorWeeklySchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSessionSlots_DoctorWeeklySchedules_DoctorWeeklyScheduleId",
                table: "DoctorSessionSlots");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSessionSlots_DoctorWeeklyScheduleId_StartTime",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "DoctorWeeklyScheduleId",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "DoctorSessionSlots");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "DoctorSessionSlots");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "DoctorSessionSlots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "DoctorSessionSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SlotStatus",
                table: "DoctorSessionSlots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "DoctorSessionSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSessionSlots_DoctorId_StartDateTime",
                table: "DoctorSessionSlots",
                columns: new[] { "DoctorId", "StartDateTime" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSessionSlots_AspNetUsers_DoctorId",
                table: "DoctorSessionSlots",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
