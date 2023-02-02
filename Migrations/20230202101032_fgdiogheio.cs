using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolRegistrationForm.Migrations
{
    public partial class fgdiogheio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Questions");
        }
    }
}
