using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _Project_.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExampleText = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ExampleValueObject_ExampleValue = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ExampleStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExampleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExampleText = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ExampleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExampleItems_Examples_ExampleId",
                        column: x => x.ExampleId,
                        principalTable: "Examples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExampleItems_ExampleId",
                table: "ExampleItems",
                column: "ExampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExampleItems");

            migrationBuilder.DropTable(
                name: "Examples");
        }
    }
}
