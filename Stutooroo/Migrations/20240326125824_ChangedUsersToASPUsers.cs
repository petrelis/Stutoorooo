using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stutooroo.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUsersToASPUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CustomerUsers_CustomerUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TutorUsers_TutorUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteListings_CustomerUsers_CustomerUserId",
                table: "FavoriteListings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_TutorUsers_PostedByTutorUserId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "CustomerUsers");

            migrationBuilder.DropTable(
                name: "TutorUsers");

            migrationBuilder.DropIndex(
                name: "IX_Listings_PostedByTutorUserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteListings_CustomerUserId",
                table: "FavoriteListings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CustomerUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TutorUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostedByTutorUserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CustomerUserId",
                table: "FavoriteListings");

            migrationBuilder.DropColumn(
                name: "CustomerUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TutorUserId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "PostedByUserId",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FavoriteListings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            /*migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");*/

            migrationBuilder.CreateIndex(
                name: "IX_Listings_PostedByUserId",
                table: "Listings",
                column: "PostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteListings_UserId",
                table: "FavoriteListings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AspNetUserId",
                table: "Comments",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AspNetUserId",
                table: "Comments",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteListings_AspNetUsers_UserId",
                table: "FavoriteListings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AspNetUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteListings_AspNetUsers_UserId",
                table: "FavoriteListings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_PostedByUserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_PostedByUserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteListings_UserId",
                table: "FavoriteListings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AspNetUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostedByUserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavoriteListings");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "PostedByTutorUserId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerUserId",
                table: "FavoriteListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerUserId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TutorUserId",
                table: "Comments",
                type: "int",
                nullable: true);

            /*migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);*/

            migrationBuilder.CreateTable(
                name: "CustomerUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TutorUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_PostedByTutorUserId",
                table: "Listings",
                column: "PostedByTutorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteListings_CustomerUserId",
                table: "FavoriteListings",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerUserId",
                table: "Comments",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TutorUserId",
                table: "Comments",
                column: "TutorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CustomerUsers_CustomerUserId",
                table: "Comments",
                column: "CustomerUserId",
                principalTable: "CustomerUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TutorUsers_TutorUserId",
                table: "Comments",
                column: "TutorUserId",
                principalTable: "TutorUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteListings_CustomerUsers_CustomerUserId",
                table: "FavoriteListings",
                column: "CustomerUserId",
                principalTable: "CustomerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_TutorUsers_PostedByTutorUserId",
                table: "Listings",
                column: "PostedByTutorUserId",
                principalTable: "TutorUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
