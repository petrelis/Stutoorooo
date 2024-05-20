using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stutooroo.Migrations
{
    /// <inheritdoc />
    public partial class ListingNumOfRatingsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "PostedByUserId",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "NumOfRatings",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "NumOfRatings",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "PostedByUserId",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
