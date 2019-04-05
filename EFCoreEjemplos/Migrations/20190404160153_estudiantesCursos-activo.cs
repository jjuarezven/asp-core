using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class estudiantesCursosactivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EstudiantesCursos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstudiantesCursos");
        }
    }
}
