using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposTecnicos",
                columns: table => new
                {
                    TipoTecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTecnicos", x => x.TipoTecnicoId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    SueldoHora = table.Column<double>(type: "REAL", nullable: false),
                    TiposTecnicosId = table.Column<int>(type: "INTEGER", nullable: false),
                    TiposTecnicosTipoTecnicoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Nombre);
                    table.ForeignKey(
                        name: "FK_Tecnicos_TiposTecnicos_TiposTecnicosId",
                        column: x => x.TiposTecnicosId,
                        principalTable: "TiposTecnicos",
                        principalColumn: "TipoTecnicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tecnicos_TiposTecnicos_TiposTecnicosTipoTecnicoId",
                        column: x => x.TiposTecnicosTipoTecnicoId,
                        principalTable: "TiposTecnicos",
                        principalColumn: "TipoTecnicoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_TiposTecnicosId",
                table: "Tecnicos",
                column: "TiposTecnicosId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_TiposTecnicosTipoTecnicoId",
                table: "Tecnicos",
                column: "TiposTecnicosTipoTecnicoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "TiposTecnicos");
        }
    }
}
