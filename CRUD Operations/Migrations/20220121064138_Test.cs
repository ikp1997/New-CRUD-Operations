using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Operations.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardTable",
                columns: table => new
                {
                    cardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(name: "Card Name", type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(name: "Image Path", type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTable", x => x.cardId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardTable");
        }
    }
}
