using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace liivlabs_infrastructure.Migrations
{
    public partial class videotext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    OriginalSize = table.Column<decimal>(nullable: false),
                    VideoFileName = table.Column<string>(nullable: true),
                    AudioFileName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    editedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
