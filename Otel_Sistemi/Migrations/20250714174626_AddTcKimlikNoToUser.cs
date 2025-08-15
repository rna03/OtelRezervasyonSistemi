using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otel_Sistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddTcKimlikNoToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Users");
        }
    }
}
