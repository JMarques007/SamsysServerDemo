using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamsysDemo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Clients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Clients");
        }
    }
}
