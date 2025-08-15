using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otel_Sistemi.Migrations
{
    /// <inheritdoc />
    public partial class RoomServiceUsesOdaTuruInsteadOfRoomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OdaTuru",
                table: "RoomServices",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "OdaTuru",
                table: "RoomServices");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
