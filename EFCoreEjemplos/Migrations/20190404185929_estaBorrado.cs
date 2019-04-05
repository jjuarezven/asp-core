using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class estaBorrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaBorrado",
                table: "Estudiantes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaBorrado",
                table: "Estudiantes");
        }
    }
}
