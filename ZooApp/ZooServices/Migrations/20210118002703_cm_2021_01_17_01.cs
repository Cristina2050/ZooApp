using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooServices.Migrations
{
    public partial class cm_2021_01_17_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BREED",
                columns: table => new
                {
                    BREED_ID_PK = table.Column<short>(nullable: false, comment: "Clave principal de identificación del registro")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BREED_NAME = table.Column<string>(unicode: false, maxLength: 64, nullable: false, comment: "Corresponde al nombre de la raza de un animal.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BREED", x => x.BREED_ID_PK);
                },
                comment: "TABLA QUE ALMACENA LOS TIPO DE RAZAS DE ANIMALES");

            migrationBuilder.CreateTable(
                name: "ANIMAL",
                columns: table => new
                {
                    ANIM_ID_PK = table.Column<int>(nullable: false, comment: "Clave principal de identificación del registro")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BREED_ID_PK = table.Column<short>(nullable: false, comment: "Corresponde a la clave foránea de razas de animales."),
                    ANIM_NAME = table.Column<string>(unicode: false, maxLength: 128, nullable: false, comment: "Corresponde al nombre del animal"),
                    ANIM_AGE = table.Column<short>(nullable: false, comment: "Edad del animal."),
                    ANIM_WEIGHT = table.Column<decimal>(type: "decimal(12,2)", nullable: false, comment: "Peso del animal."),
                    ANIM_CHARACTERISTIC = table.Column<string>(unicode: false, maxLength: 512, nullable: true, comment: "Corresponde a las características del animal"),
                    ANIM_USER = table.Column<string>(unicode: false, maxLength: 32, nullable: false, comment: "Corresponde al usuario que transacciona con el registro. "),
                    ANIM_CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Corresponde a la fecha de creación del registro"),
                    ANIM_UPDATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Corresponde a la fecha de actualización del registro")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANIMAL", x => x.ANIM_ID_PK);
                    table.ForeignKey(
                        name: "FK_ANIMAL_BREED",
                        column: x => x.BREED_ID_PK,
                        principalTable: "BREED",
                        principalColumn: "BREED_ID_PK",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabla que almacena los animales del zoológico.");

            migrationBuilder.CreateIndex(
                name: "ANIM_BREED_INDX",
                table: "ANIMAL",
                column: "ANIM_ID_PK");

            migrationBuilder.CreateIndex(
                name: "BREED_ANIM_INDX",
                table: "ANIMAL",
                column: "BREED_ID_PK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANIMAL");

            migrationBuilder.DropTable(
                name: "BREED");
        }
    }
}
