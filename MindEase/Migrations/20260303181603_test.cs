using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "UserDoctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "UserDoctor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
