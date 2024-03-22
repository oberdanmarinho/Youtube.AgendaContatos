using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaContatos.Migrations
{
    /// <inheritdoc />
    public partial class OM110320240855 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Contatos",
                newName: "Telefone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Contatos",
                newName: "Celular");
        }
    }
}
