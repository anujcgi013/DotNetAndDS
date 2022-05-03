using Microsoft.EntityFrameworkCore.Migrations;

namespace EatHealthyApi.Migrations
{
    public partial class VarietyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VarietyType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false),
                    Descritpion = table.Column<int>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<string>(maxLength: 100, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VarietyType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VarietyType");
        }
    }
}
