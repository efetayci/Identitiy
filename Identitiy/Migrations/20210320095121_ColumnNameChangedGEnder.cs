using Microsoft.EntityFrameworkCore.Migrations;

namespace Identitiy.Migrations
{
    public partial class ColumnNameChangedGEnder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cinsiyet",
                table: "AspNetUsers",
                newName: "Gender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "Cinsiyet");
        }
    }
}
