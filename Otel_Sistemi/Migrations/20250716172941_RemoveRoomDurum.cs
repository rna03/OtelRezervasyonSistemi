using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otel_Sistemi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomDurum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Rooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
       

            migrationBuilder.AddColumn<string>(
                name: "Durum",
                table: "Rooms",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

         
        }
    }
}
