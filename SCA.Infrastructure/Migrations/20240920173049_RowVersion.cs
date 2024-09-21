using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "Group",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "Connector",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "ChargeStation",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Connector");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ChargeStation");
        }
    }
}
