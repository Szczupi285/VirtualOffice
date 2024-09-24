using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualOffice.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class PrivateDocumentDrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateDocuments__attachmentFilePaths");

            migrationBuilder.DropTable(
                name: "PublicDocuments__attachmentFilePaths");

            migrationBuilder.DropTable(
                name: "PrivateDocuments");

            migrationBuilder.CreateTable(
                name: "DocumentFilePath",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFilePath", x => new { x.PublicDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_DocumentFilePath_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentFilePath");

            migrationBuilder.CreateTable(
                name: "PrivateDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicDocuments__attachmentFilePaths",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDocuments__attachmentFilePaths", x => new { x.PublicDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PublicDocuments__attachmentFilePaths_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateDocuments__attachmentFilePaths",
                columns: table => new
                {
                    PrivateDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDocuments__attachmentFilePaths", x => new { x.PrivateDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PrivateDocuments__attachmentFilePaths_PrivateDocuments_PrivateDocumentId",
                        column: x => x.PrivateDocumentId,
                        principalTable: "PrivateDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
