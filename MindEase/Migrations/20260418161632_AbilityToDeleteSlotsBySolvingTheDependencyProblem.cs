using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class AbilityToDeleteSlotsBySolvingTheDependencyProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DoctorSessionSlotId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorSessionSlotId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorSessionSlotId",
                table: "Bookings",
                column: "DoctorSessionSlotId",
                unique: true,
                filter: "[DoctorSessionSlotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings",
                column: "DoctorSessionSlotId",
                principalTable: "DoctorSessionSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DoctorSessionSlotId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorSessionSlotId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorSessionSlotId",
                table: "Bookings",
                column: "DoctorSessionSlotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings",
                column: "DoctorSessionSlotId",
                principalTable: "DoctorSessionSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
