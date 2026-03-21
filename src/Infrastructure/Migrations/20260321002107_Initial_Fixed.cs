using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_habilitaions",
                columns: table => new
                {
                    HabilitationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_habilitaions", x => x.HabilitationId);
                });

            migrationBuilder.CreateTable(
                name: "tb_holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFixedDate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    National = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_holidays", x => x.Id);
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
                name: "tb_teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Registration = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_teachers", x => x.TeacherId);
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
                name: "tb_classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_tb_classes_tb_classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "tb_classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tb_classes_tb_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "tb_teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tb_teacher_habilitaions",
                columns: table => new
                {
                    TeacherHabilitationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    HabilitationId = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_teacher_habilitaions", x => x.TeacherHabilitationId);
                    table.ForeignKey(
                        name: "FK_tb_teacher_habilitaions_tb_habilitaions_HabilitationId",
                        column: x => x.HabilitationId,
                        principalTable: "tb_habilitaions",
                        principalColumn: "HabilitationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tb_teacher_habilitaions_tb_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "tb_teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.CreateTable(
                name: "tb_schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_tb_schedules_tb_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "tb_classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tb_schedules_tb_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "tb_teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tb_attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_attendances_tb_schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "tb_schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tb_attendances_tb_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tb_students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendances_Date",
                table: "tb_attendances",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendances_ScheduleId",
                table: "tb_attendances",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendances_ScheduleId_StudentId",
                table: "tb_attendances",
                columns: new[] { "ScheduleId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendances_Status",
                table: "tb_attendances",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendances_StudentId",
                table: "tb_attendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_classes_ClassroomId",
                table: "tb_classes",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_classes_StartDate_EndDate",
                table: "tb_classes",
                columns: new[] { "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_classes_TeacherId",
                table: "tb_classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_classrooms_Name",
                table: "tb_classrooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_habilitaions_Name",
                table: "tb_habilitaions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_holidays_Day",
                table: "tb_holidays",
                column: "Day",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_holidays_IsFixedDate",
                table: "tb_holidays",
                column: "IsFixedDate");

            migrationBuilder.CreateIndex(
                name: "IX_tb_holidays_National",
                table: "tb_holidays",
                column: "National");

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_ClassId",
                table: "tb_schedules",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_ClassId_Date",
                table: "tb_schedules",
                columns: new[] { "ClassId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_Date",
                table: "tb_schedules",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_DayOfWeek",
                table: "tb_schedules",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_IsHoliday",
                table: "tb_schedules",
                column: "IsHoliday");

            migrationBuilder.CreateIndex(
                name: "IX_tb_schedules_TeacherId",
                table: "tb_schedules",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_students_RfidCardId",
                table: "tb_students",
                column: "RfidCardId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_teacher_habilitaions_HabilitationId",
                table: "tb_teacher_habilitaions",
                column: "HabilitationId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_teacher_habilitaions_TeacherId",
                table: "tb_teacher_habilitaions",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_teachers_Email",
                table: "tb_teachers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_teachers_Registration",
                table: "tb_teachers",
                column: "Registration",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_roles_UserId",
                table: "tb_user_roles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_attendances");

            migrationBuilder.DropTable(
                name: "tb_holidays");

            migrationBuilder.DropTable(
                name: "tb_refreshtoken");

            migrationBuilder.DropTable(
                name: "tb_register_logs");

            migrationBuilder.DropTable(
                name: "tb_roles");

            migrationBuilder.DropTable(
                name: "tb_teacher_habilitaions");

            migrationBuilder.DropTable(
                name: "tb_user_roles");

            migrationBuilder.DropTable(
                name: "tb_schedules");

            migrationBuilder.DropTable(
                name: "tb_students");

            migrationBuilder.DropTable(
                name: "tb_habilitaions");

            migrationBuilder.DropTable(
                name: "tb_users");

            migrationBuilder.DropTable(
                name: "tb_classes");

            migrationBuilder.DropTable(
                name: "tb_rfid_cards");

            migrationBuilder.DropTable(
                name: "tb_classrooms");

            migrationBuilder.DropTable(
                name: "tb_teachers");
        }
    }
}
