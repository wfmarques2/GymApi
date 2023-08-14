using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApi.Migrations
{
    /// <inheritdoc />
    public partial class GymDatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Workouts_WorkoutId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_WorkoutId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Workouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ClientId",
                table: "Workouts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Clients_ClientId",
                table: "Workouts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Clients_ClientId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ClientId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Workouts");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_WorkoutId",
                table: "Clients",
                column: "WorkoutId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Workouts_WorkoutId",
                table: "Clients",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
