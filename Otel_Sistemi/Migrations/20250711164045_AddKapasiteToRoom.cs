using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otel_Sistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddKapasiteToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kapasite",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kapasite",
                table: "Rooms");
        }
    }
}
