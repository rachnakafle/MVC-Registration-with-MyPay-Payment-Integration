using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolRegistrationForm.Migrations
{
    public partial class gdgd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Questions");
        }
    }
}
