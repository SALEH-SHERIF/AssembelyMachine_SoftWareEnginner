using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblerMachine.Migrations
{
    /// <inheritdoc />
    public partial class CarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WheelNumber = table.Column<int>(type: "int", nullable: false),
                    DoorNumber = table.Column<int>(type: "int", nullable: false),
                    GlassNumber = table.Column<int>(type: "int", nullable: false),
                    MotorNumber = table.Column<int>(type: "int", nullable: false),
                    WheelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotorType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
