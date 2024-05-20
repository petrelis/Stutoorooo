using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stutooroo.Migrations
{
    /// <inheritdoc />
    public partial class AddedListingRatingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfRatings",
                table: "Listings");

            migrationBuilder.CreateTable(
                name: "ListingRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingRatings_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListingRatings_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingRatings_AspNetUserId",
                table: "ListingRatings",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingRatings_ListingId",
                table: "ListingRatings",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingRatings");

            migrationBuilder.AddColumn<int>(
                name: "NumOfRatings",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
