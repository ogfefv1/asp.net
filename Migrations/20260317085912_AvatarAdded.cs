using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspKnP231.Migrations
{
    /// <inheritdoc />
    public partial class AvatarAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFilename",
                table: "UserAccesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserAccesses",
                keyColumn: "Id",
                keyValue: new Guid("f749f994-af12-4ea1-8bef-829ef751fc4a"),
                column: "AvatarFilename",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFilename",
                table: "UserAccesses");
        }
    }
}
