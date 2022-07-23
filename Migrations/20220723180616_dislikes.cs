using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace watchify.Migrations
{
    public partial class dislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Users_LikedById",
                table: "UserVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Videos_LikedVideosId",
                table: "UserVideo");

            migrationBuilder.RenameColumn(
                name: "LikedVideosId",
                table: "UserVideo",
                newName: "DislikedVideosId");

            migrationBuilder.RenameColumn(
                name: "LikedById",
                table: "UserVideo",
                newName: "DislikedById");

            migrationBuilder.RenameIndex(
                name: "IX_UserVideo_LikedVideosId",
                table: "UserVideo",
                newName: "IX_UserVideo_DislikedVideosId");

            migrationBuilder.CreateTable(
                name: "UserVideo1",
                columns: table => new
                {
                    LikedById = table.Column<Guid>(type: "uuid", nullable: false),
                    LikedVideosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo1", x => new { x.LikedById, x.LikedVideosId });
                    table.ForeignKey(
                        name: "FK_UserVideo1_Users_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideo1_Videos_LikedVideosId",
                        column: x => x.LikedVideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo1_LikedVideosId",
                table: "UserVideo1",
                column: "LikedVideosId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Users_DislikedById",
                table: "UserVideo",
                column: "DislikedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Videos_DislikedVideosId",
                table: "UserVideo",
                column: "DislikedVideosId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Users_DislikedById",
                table: "UserVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Videos_DislikedVideosId",
                table: "UserVideo");

            migrationBuilder.DropTable(
                name: "UserVideo1");

            migrationBuilder.RenameColumn(
                name: "DislikedVideosId",
                table: "UserVideo",
                newName: "LikedVideosId");

            migrationBuilder.RenameColumn(
                name: "DislikedById",
                table: "UserVideo",
                newName: "LikedById");

            migrationBuilder.RenameIndex(
                name: "IX_UserVideo_DislikedVideosId",
                table: "UserVideo",
                newName: "IX_UserVideo_LikedVideosId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Users_LikedById",
                table: "UserVideo",
                column: "LikedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Videos_LikedVideosId",
                table: "UserVideo",
                column: "LikedVideosId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
