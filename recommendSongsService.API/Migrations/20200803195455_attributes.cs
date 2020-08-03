using Microsoft.EntityFrameworkCore.Migrations;

namespace recommendSongsService.Migrations
{
    public partial class attributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgotPasswordCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "Users");
        }
    }
}
