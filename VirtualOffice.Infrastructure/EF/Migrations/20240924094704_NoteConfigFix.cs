using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualOffice.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class NoteConfigFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Employees_CreatedByUserId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Notes",
                newName: "_createdBy");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_CreatedByUserId",
                table: "Notes",
                newName: "IX_Notes__createdBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Employees__createdBy",
                table: "Notes",
                column: "_createdBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Employees__createdBy",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "_createdBy",
                table: "Notes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes__createdBy",
                table: "Notes",
                newName: "IX_Notes_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Employees_CreatedByUserId",
                table: "Notes",
                column: "CreatedByUserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
