using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidekVideoAPI.Migrations
{
    public partial class foreignkeysadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThumbnailViewItem",
                table: "ThumbnailViewItem");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ThumbnailViewItem",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThumbnailViewItem",
                table: "ThumbnailViewItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailViewItem_VideoId",
                table: "ThumbnailViewItem",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThumbnailViewItem",
                table: "ThumbnailViewItem");

            migrationBuilder.DropIndex(
                name: "IX_ThumbnailViewItem_VideoId",
                table: "ThumbnailViewItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ThumbnailViewItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThumbnailViewItem",
                table: "ThumbnailViewItem",
                column: "VideoId");
        }
    }
}
