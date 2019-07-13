using Microsoft.EntityFrameworkCore.Migrations;

namespace liivlabs_infrastructure.Migrations
{
    public partial class parent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentUserId",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "User");
        }
    }
}
