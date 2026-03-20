using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_rfid_cards",
                columns: table => new
                {
                    RfidCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rfid_cards", x => x.RfidCardId);
                });

            migrationBuilder.CreateTable(
                name: "tb_refreshtoken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "varchar(max)", nullable: false),
                    CreatAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_refreshtoken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_register_logs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "varchar(max)", nullable: false),
                    Details = table.Column<string>(type: "varchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "varchar(max)", nullable: false),
                    Exception = table.Column<string>(type: "varchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "varchar(max)", nullable: false),
                    Inner = table.Column<string>(type: "varchar(max)", nullable: false),
                    Situation = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_register_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Login = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(225)", nullable: false),
                    Salt = table.Column<string>(type: "varchar(225)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    FailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tb_students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Registration = table.Column<string>(type: "varchar(20)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    RfidCardId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_tb_students_tb_rfid_cards_RfidCardId",
                        column: x => x.RfidCardId,
                        principalTable: "tb_rfid_cards",
                        principalColumn: "RfidCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_user_roles",
                columns: table => new
                {
                    UserRolesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user_roles", x => x.UserRolesId);
                    table.ForeignKey(
                        name: "FK_tb_user_roles_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_roles_UserId",
                table: "tb_user_roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_students_RfidCardId",
                table: "tb_students",
                column: "RfidCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_refreshtoken");

            migrationBuilder.DropTable(
                name: "tb_register_logs");

            migrationBuilder.DropTable(
                name: "tb_roles");

            migrationBuilder.DropTable(
                name: "tb_user_roles");

            migrationBuilder.DropTable(
                name: "tb_students");

            migrationBuilder.DropTable(
                name: "tb_users");

            migrationBuilder.DropTable(
                name: "tb_rfid_cards");
        }
    }
}
