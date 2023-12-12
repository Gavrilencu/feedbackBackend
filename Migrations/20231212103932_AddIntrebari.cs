using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace feedback.Migrations
{
    /// <inheritdoc />
    public partial class AddIntrebari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersoanaAdresata",
                table: "Chestionare");

            migrationBuilder.CreateTable(
                name: "Intrebari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DenumireIntrebare = table.Column<string>(type: "TEXT", nullable: false),
                    IdChestionar = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intrebari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intrebari");

            migrationBuilder.AddColumn<string>(
                name: "PersoanaAdresata",
                table: "Chestionare",
                type: "TEXT",
                nullable: true);
        }
    }
}
