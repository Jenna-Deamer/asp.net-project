using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newAspProject.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightAndWeightTypeToColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnit",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "products");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                table: "products");
        }
    }
}
