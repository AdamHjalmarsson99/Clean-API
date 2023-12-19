using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSeedHelper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "CanFly", "Name" },
                values: new object[,]
                {
                    { new Guid("0d94352d-4cec-44e6-a101-8375092c89a5"), true, "Mitrovic" },
                    { new Guid("4c344ccd-961a-4231-b59f-2f700c49736c"), false, "Klose" },
                    { new Guid("6d5b0ba1-3b12-4b6b-8111-835230a8fe0b"), true, "Gomez" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("00e7a653-c570-4a67-a8fc-0af3ead19d62"), true, "Santi Cazorla" },
                    { new Guid("3b650789-d4df-4d6b-b3c6-5917b3fffcb4"), true, "Sneijder" },
                    { new Guid("68ed9d86-73bf-4246-bd60-6590b17f3829"), false, "Cambiasso" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0a149b39-a81e-4973-85a0-e7db0425117e"), "Mertesacker" },
                    { new Guid("306109b2-a4a2-40f3-8401-32e5ec1dc5ba"), "Saliba" },
                    { new Guid("938ba316-7510-48f6-9c9c-d8ed56821240"), "Nesta" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("42ce63d3-509c-4dc9-902a-d037db4f3a93"), "Boss", "Admin" },
                    { new Guid("f2423811-d5be-4f0f-8221-698ffa3ad615"), "noob", "noob" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("0d94352d-4cec-44e6-a101-8375092c89a5"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("4c344ccd-961a-4231-b59f-2f700c49736c"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("6d5b0ba1-3b12-4b6b-8111-835230a8fe0b"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("00e7a653-c570-4a67-a8fc-0af3ead19d62"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("3b650789-d4df-4d6b-b3c6-5917b3fffcb4"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("68ed9d86-73bf-4246-bd60-6590b17f3829"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("0a149b39-a81e-4973-85a0-e7db0425117e"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("306109b2-a4a2-40f3-8401-32e5ec1dc5ba"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("938ba316-7510-48f6-9c9c-d8ed56821240"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42ce63d3-509c-4dc9-902a-d037db4f3a93"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2423811-d5be-4f0f-8221-698ffa3ad615"));
        }
    }
}
