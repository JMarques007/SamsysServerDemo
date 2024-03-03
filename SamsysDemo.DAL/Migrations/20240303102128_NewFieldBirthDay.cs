using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamsysDemo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldBirthDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Clients",
                newName: "Birthday");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Clients",
                newName: "BirthDate");
        }
    }
}
