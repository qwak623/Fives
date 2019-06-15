using Microsoft.EntityFrameworkCore.Migrations;

namespace Fives.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fives",
                columns: table => new
                {
                    Cards = table.Column<string>(nullable: false),
                    Provinces = table.Column<int>(nullable: false),
                    Duchies = table.Column<int>(nullable: false),
                    Estates = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fives", x => x.Cards);
                });

            migrationBuilder.CreateTable(
                name: "BuyMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    AgendaDbCards = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyMenuItem_Fives_AgendaDbCards",
                        column: x => x.AgendaDbCards,
                        principalTable: "Fives",
                        principalColumn: "Cards",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyMenuItem_AgendaDbCards",
                table: "BuyMenuItem",
                column: "AgendaDbCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyMenuItem");

            migrationBuilder.DropTable(
                name: "Fives");
        }
    }
}
