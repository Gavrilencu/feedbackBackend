using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace feedback.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserResponsesWithChestionarId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdChestionar = table.Column<int>(type: "INTEGER", nullable: false),
                    Raspuns = table.Column<string>(type: "TEXT", nullable: false),
                    FromPerson = table.Column<string>(type: "TEXT", nullable: false),
                    ForPerson = table.Column<string>(type: "TEXT", nullable: false),
                    EncodedName = table.Column<string>(type: "TEXT", nullable: false),
                    PostDate = table.Column<string>(type: "TEXT", nullable: true)
                    // Added this line
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResponse", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResponse");
        }
    }
}
