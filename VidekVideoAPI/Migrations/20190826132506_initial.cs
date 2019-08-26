using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidekVideoAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Descirption = table.Column<string>(nullable: true),
                    SourcePath = table.Column<string>(nullable: true),
                    StreamURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoID = table.Column<int>(nullable: false),
                    SourcePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnails_Video_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThumbnailViewItem",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ThumbnailURL = table.Column<string>(nullable: true),
                    VideoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThumbnailViewItem", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_ThumbnailViewItem_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_VideoID",
                table: "Thumbnails",
                column: "VideoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnails");

            migrationBuilder.DropTable(
                name: "ThumbnailViewItem");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
