using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdToUserDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "UserDoctor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "UserDoctor");
        }
    }
}
