using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadLoop.api.Migrations
{
    /// <inheritdoc />
    public partial class InitiaialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER_TABLE",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    USER_PASSWORD_HASH = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    USER_AGE = table.Column<int>(type: "int", nullable: false),
                    USER_ROLE = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    USER_NAME = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValue: "None")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER_TAB__F3BEEBFFE4517502", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "TRUCK",
                columns: table => new
                {
                    TRUCK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRUCK_IMAGE = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    TRUCK_LICENSE_PLATE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    OWNER_ROLE = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValue: "T"),
                    TRUCK_CAPACITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TRUCK__6693F6336554B9A6", x => x.TRUCK_ID);
                    table.ForeignKey(
                        name: "FK__TRUCK__USER_ID__540C7B00",
                        column: x => x.USER_ID,
                        principalTable: "USER_TABLE",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRUCK_USER_ID",
                table: "TRUCK",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__USER_TAB__43CA3168221749CA",
                table: "USER_TABLE",
                column: "USER_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_USER_EMAIL_ROLE",
                table: "USER_TABLE",
                columns: new[] { "USER_EMAIL", "USER_ROLE" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRUCK");

            migrationBuilder.DropTable(
                name: "USER_TABLE");
        }
    }
}
