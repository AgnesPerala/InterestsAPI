using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterestsAPI.Migrations
{
    /// <inheritdoc />
    public partial class _2tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PesronName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_Interests_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Webbsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interests_PersonId",
                table: "Interests",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_InterestId",
                table: "Links",
                column: "InterestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
