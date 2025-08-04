using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations.NZWalksAuthDb
{
    /// <inheritdoc />
    public partial class SeetingDataAuthDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5993E051-F7F1-4B89-93EA-E7F395461F9D", "5993E051-F7F1-4B89-93EA-E7F395461F9D", "readerId", "READERID" },
                    { "EC11B02C-A2C6-4753-BC2F-DD32C4A95EF5", "EC11B02C-A2C6-4753-BC2F-DD32C4A95EF5", "writerId", "WRITERID" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5993E051-F7F1-4B89-93EA-E7F395461F9D");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EC11B02C-A2C6-4753-BC2F-DD32C4A95EF5");
        }
    }
}
