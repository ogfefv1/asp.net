using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspKnP231.Migrations
{
    /// <inheritdoc />
    public partial class P_S_Constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopSections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopSections_Slug",
                table: "ShopSections",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_ShopSectionId",
                table: "ShopProducts",
                column: "ShopSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_Slug",
                table: "ShopProducts",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProducts_ShopSections_ShopSectionId",
                table: "ShopProducts",
                column: "ShopSectionId",
                principalTable: "ShopSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProducts_ShopSections_ShopSectionId",
                table: "ShopProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShopSections_Slug",
                table: "ShopSections");

            migrationBuilder.DropIndex(
                name: "IX_ShopProducts_ShopSectionId",
                table: "ShopProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShopProducts_Slug",
                table: "ShopProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopSections",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
