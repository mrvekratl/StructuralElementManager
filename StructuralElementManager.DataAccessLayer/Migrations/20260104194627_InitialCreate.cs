using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StructuralElementManager.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructuralMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Density = table.Column<double>(type: "float", nullable: false),
                    CompressiveStrength = table.Column<double>(type: "float", nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuralMaterials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "StructuralElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    ElementType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    StructuralBeam_Width = table.Column<double>(type: "float", nullable: true),
                    StructuralBeam_Height = table.Column<double>(type: "float", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Depth = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    StructuralSlab_Length = table.Column<double>(type: "float", nullable: true),
                    StructuralSlab_Width = table.Column<double>(type: "float", nullable: true),
                    Thickness = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuralElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructuralElement_StructuralMaterials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "StructuralMaterials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StructuralMaterials",
                columns: new[] { "MaterialId", "CompressiveStrength", "Density", "MaterialName", "MaterialType" },
                values: new object[,]
                {
                    { 1, 30.0, 2.5, "C30 Concrete", 0 },
                    { 2, 35.0, 2.5, "C35 Concrete", 0 },
                    { 3, 420.0, 7.8499999999999996, "S420 Steel", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructuralElement_MaterialID",
                table: "StructuralElement",
                column: "MaterialID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructuralElement");

            migrationBuilder.DropTable(
                name: "StructuralMaterials");
        }
    }
}
