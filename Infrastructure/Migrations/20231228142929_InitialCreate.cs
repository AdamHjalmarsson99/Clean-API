using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    CanFly = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Color = table.Column<string>(type: "longtext", nullable: true),
                    LikesToPlay = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Breed = table.Column<string>(type: "longtext", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Dog_Breed = table.Column<string>(type: "longtext", nullable: true),
                    Dog_Weight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalModel", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalModelUser",
                columns: table => new
                {
                    AnimalsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsersId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalModelUser", x => new { x.AnimalsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AnimalModelUser_AnimalModel_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalModelUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("1c954738-517d-4a58-a9c1-606195624b87"), "Leonberger", "Dog", "Saliba", 50 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("355bdbe2-faa4-4c28-bc5d-52076c3fa7ed"), false, "Red", "Bird", "Klose" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[] { new Guid("4ee3ea4e-c5d5-4ae0-bbc4-1a3e7618c1c3"), "Bengal", "Cat", true, "Sneijder", 5 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("8641b26d-f4c0-493e-8536-dcfd02cec812"), true, "Blue", "Bird", "Gomez" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("a26abba6-1028-48d7-a8ad-a24c06998e31"), "Berner senner", "Dog", "Nesta", 35 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("c3bcf725-46d5-4e33-889d-f61ad77bcd82"), true, "White", "Bird", "Mitrovic" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("c8564b38-6cbe-41fa-b271-c8e3cbd1d18c"), "Great Dane", "Dog", "Mertesacker", 75 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec3fd5ef-455c-4d10-9241-a3824054a6e7"), "Burma", "Cat", true, "Santi Cazorla", 7 },
                    { new Guid("fd494355-d59f-4386-b336-bbca8533ac6e"), "Maine Coon", "Cat", false, "Cambiasso", 12 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("8f640902-755f-4c9e-85a7-9767d9248158"), "Boss", "Admin" },
                    { new Guid("dc8235aa-0cae-4ab9-afb9-8866f7e1be2e"), "noob", "noob" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalModelUser_UsersId",
                table: "AnimalModelUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalModelUser");

            migrationBuilder.DropTable(
                name: "AnimalModel");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
