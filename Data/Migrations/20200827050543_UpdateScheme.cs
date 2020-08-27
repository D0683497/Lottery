using Microsoft.EntityFrameworkCore.Migrations;

namespace Lottery.Data.Migrations
{
    public partial class UpdateScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<string>(nullable: false),
                    RoundName = table.Column<string>(nullable: true),
                    RoundComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                });

            migrationBuilder.CreateTable(
                name: "Winners",
                columns: table => new
                {
                    WinnerId = table.Column<string>(nullable: false),
                    RoundId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winners", x => x.WinnerId);
                    table.ForeignKey(
                        name: "FK_Winners_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    AttendeeId = table.Column<string>(nullable: false),
                    AttendeeNID = table.Column<string>(nullable: true),
                    AttendeeName = table.Column<string>(nullable: true),
                    AttendeeDepartment = table.Column<string>(nullable: true),
                    WinnerId = table.Column<string>(nullable: true),
                    RoundId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.AttendeeId);
                    table.ForeignKey(
                        name: "FK_Attendees_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendees_Winners_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Winners",
                        principalColumn: "WinnerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    PrizeId = table.Column<string>(nullable: false),
                    PrizeName = table.Column<string>(nullable: true),
                    PrizeImage = table.Column<string>(nullable: true),
                    PrizeNumber = table.Column<int>(nullable: false),
                    PrizeComplete = table.Column<bool>(nullable: false),
                    PrizeOrder = table.Column<int>(nullable: false),
                    RoundId = table.Column<string>(nullable: true),
                    WinnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.PrizeId);
                    table.ForeignKey(
                        name: "FK_Prizes_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prizes_Winners_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Winners",
                        principalColumn: "WinnerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_RoundId",
                table: "Attendees",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_WinnerId",
                table: "Attendees",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prizes_RoundId",
                table: "Prizes",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Prizes_WinnerId",
                table: "Prizes",
                column: "WinnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winners_RoundId",
                table: "Winners",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
