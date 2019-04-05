using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class agregandoedad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Estudiantes");
        }
    }
}
