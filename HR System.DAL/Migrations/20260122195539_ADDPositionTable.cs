using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ADDPositionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_PositionId",
                table: "employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_positions_PositionId",
                table: "employees");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropIndex(
                name: "IX_employees_PositionId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "employees");
        }
    }
}
