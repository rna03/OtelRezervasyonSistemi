using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otel_Sistemi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomIdColumnFromRoomService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices");

            migrationBuilder.DropIndex(
                name: "IX_RoomServices_RoomId",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomServices");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
