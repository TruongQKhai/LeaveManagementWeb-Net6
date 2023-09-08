using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddPeriodColumnToAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "dac5a03a-67d4-48af-88c5-baf48aa58cb7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "e72c82a0-8679-460c-95f7-2d11c5fcbbc6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d392d73-41f6-44cb-89ee-eaea224a84f3", "AQAAAAEAACcQAAAAEPVbH3HiaJMEOAlmcx7wkWIbRccrxtXoGg1nv9h9CUZLYE38s6C7deK3GEWr73v3IQ==", "8df95a9b-fc45-4004-a0ca-1671e5853474" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adbe5928-328c-416d-870e-3ee5eff204c8", "AQAAAAEAACcQAAAAEEdyG2a1emVjT23vAWh+WgCbi3i5KPu+kbvUTxxaI+PuR0iU9Y0L/5VtVydwDFYjEg==", "e1ee3930-abcd-49f2-bb5c-82a86e455ed9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "6fec917e-dd01-4956-92c9-2e344107de19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "e8334361-089c-45b7-bc4e-181805d99fb2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0d1654d-bb16-4431-8456-e246e0484129", "AQAAAAEAACcQAAAAEFJJanLBdT/q7Xpum2LMEKUpfmbmwHv+/fjKMpodTe8fEJ5w8IwEzBEYFoGxvXFS/A==", "b91c2a89-72fe-46c0-ab8a-ca6b8378e149" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21210e3f-c19a-4043-b43a-55d01a69b4ab", "AQAAAAEAACcQAAAAEA/jVkhf1/wYT5z5v2awyf1HmbKIB9zN1wxA7ax5jQcRdUYg5LA1CiCvQYxxmZ48kQ==", "d3112c27-5df7-47d6-9114-2ac602fd9ec1" });
        }
    }
}
