using Microsoft.EntityFrameworkCore.Migrations;

namespace SYR.Core.DomainModel.Migrations
{
    public partial class RemoveAllowFromSequrityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allow",
                table: "SequrityRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Allow",
                table: "SequrityRoles",
                nullable: false,
                defaultValue: false);
        }
    }
}
