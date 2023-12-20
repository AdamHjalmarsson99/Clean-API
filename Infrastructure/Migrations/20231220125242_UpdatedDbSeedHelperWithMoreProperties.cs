using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDbSeedHelperWithMoreProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Dogs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Dogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Cats",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Birds",
                type: "longtext",
                nullable: false);

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "CanFly", "Color", "Name" },
                values: new object[,]
                {
                    { new Guid("afae045c-960e-4b89-ab0b-3efef596c372"), false, "Red", "Klose" },
                    { new Guid("cf727059-dc71-48c1-abd9-21f196658126"), true, "Blue", "Gomez" },
                    { new Guid("d30022d8-53c4-4dfd-8610-6a67e6b8cd8b"), true, "White", "Mitrovic" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Breed", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("5b219ade-bee7-4eb8-9c08-7f23b04d7717"), "Burma", true, "Santi Cazorla", 7 },
                    { new Guid("62e1ae66-673f-4781-a72a-f141f0e99686"), "Maine Coon", false, "Cambiasso", 12 },
                    { new Guid("ad32fd5a-d82a-4c06-8ac9-77978987b328"), "Bengal", true, "Sneijder", 5 }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Breed", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("1e2c5c6d-7f22-4aa0-a3f2-270a7d78acd2"), "Great Dane", "Mertesacker", 75 },
                    { new Guid("e5147d7f-efe9-4b5f-8b0d-831b9a565929"), "Leonberger", "Saliba", 50 },
                    { new Guid("efe94c5e-0a9f-424d-afa5-1ff0bb49d1ba"), "Berner senner", "Nesta", 35 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("073e8895-d72a-4531-acd0-0ff414b2ba93"), "noob", "noob" },
                    { new Guid("0a78f0a7-10f1-44f3-825f-0bf1349111c0"), "Boss", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("afae045c-960e-4b89-ab0b-3efef596c372"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("cf727059-dc71-48c1-abd9-21f196658126"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("d30022d8-53c4-4dfd-8610-6a67e6b8cd8b"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("5b219ade-bee7-4eb8-9c08-7f23b04d7717"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("62e1ae66-673f-4781-a72a-f141f0e99686"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("ad32fd5a-d82a-4c06-8ac9-77978987b328"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("1e2c5c6d-7f22-4aa0-a3f2-270a7d78acd2"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("e5147d7f-efe9-4b5f-8b0d-831b9a565929"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("efe94c5e-0a9f-424d-afa5-1ff0bb49d1ba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("073e8895-d72a-4531-acd0-0ff414b2ba93"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a78f0a7-10f1-44f3-825f-0bf1349111c0"));

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Birds");

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
    }
}
