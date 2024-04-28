using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterestsAPI.Migrations
{
    /// <inheritdoc />
    public partial class changename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PesronName",
                table: "Persons",
                newName: "PersonName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Persons",
                newName: "PesronName");
        }
    }
}
