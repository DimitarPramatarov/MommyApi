using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MommyApi.Data.Migrations
{
    public partial class newEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Asnwers = table.Column<int>(type: "int", nullable: false),
                    Posts = table.Column<int>(type: "int", nullable: false),
                    Follows = table.Column<int>(type: "int", nullable: false),
                    MainPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileUserId",
                table: "AspNetUsers",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserProfiles_UserProfileUserId",
                table: "AspNetUsers",
                column: "UserProfileUserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserProfiles_UserProfileUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");
        }
    }
}
