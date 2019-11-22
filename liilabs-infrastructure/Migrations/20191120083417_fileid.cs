using Microsoft.EntityFrameworkCore.Migrations;

namespace liivlabs_infrastructure.Migrations
{
    public partial class fileid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnyNew",
                table: "FileAlert",
                newName: "IsNew");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FileAlert",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FileAlert");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "FileAlert",
                newName: "AnyNew");
        }
    }
}
