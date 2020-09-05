using Microsoft.EntityFrameworkCore.Migrations;

namespace Lottery.Data.Migrations
{
    public partial class UpdateIsAwardedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AttendeeIsAwarded",
                table: "Attendees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendeeIsAwarded",
                table: "Attendees");
        }
    }
}
