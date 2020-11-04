using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductInformation.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID", "Name" },
                values: new object[] { -1, "Kitchen" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID", "Name" },
                values: new object[] { -2, "Garage" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "CategoryID", "Name" },
                values: new object[,]
                {
                    { -1, -1, "Mixer" },
                    { -2, -1, "Rice Cooker" },
                    { -3, -2, "Wrench Set" },
                    { -4, -2, "Floor Jack" },
                    { -5, -2, "Screwdriver Set" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: -1);
        }
    }
}
