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
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                    ExampleAggregateId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExampleItems_Examples_ExampleAggregateId",
                        column: x => x.ExampleAggregateId,
                        principalTable: "Examples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExampleItems_ExampleAggregateId",
                table: "ExampleItems",
                column: "ExampleAggregateId");
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
