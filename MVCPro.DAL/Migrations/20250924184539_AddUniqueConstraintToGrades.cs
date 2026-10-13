using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCPro.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FName", "JoinedAt", "LName", "Role" },
                values: new object[,]
                {
                    { 1, "ahmed@example.com", "Ahmed", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", "Admin" },
                    { 2, "sara@example.com", "Sara", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hassan", "Student" },
                    { 3, "omar@example.com", "Omar", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mahmoud", "Student" },
                    { 4, "nour@example.com", "Nour", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fathy", "Student" },
                    { 5, "mona@example.com", "Mona", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sami", "Instructor" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "Description", "EndDate", "InstructorId", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 8, "Robots with AI", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Robots", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 0, "Basic Physics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Physics 101", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "Learn C# basics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "C# Programming", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, "Design Principles", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "UI/UX Design", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, "Basics of Management", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Business Management", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "CourseId", "Remarks", "Score", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Excellent", 95.0, 2 },
                    { 2, 2, "Good", 88.0, 2 },
                    { 3, 3, "Very Good", 92.0, 3 },
                    { 4, 1, "Good", 85.0, 4 },
                    { 5, 4, "Very Good", 90.0, 4 },
                    { 6, 5, "Satisfactory", 78.0, 3 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "CourseId", "EndTime", "Location", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room 101", new DateTime(2025, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Robots Session 1" },
                    { 2, 2, new DateTime(2025, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room 102", new DateTime(2025, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Physics Session 1" },
                    { 3, 3, new DateTime(2025, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room 103", new DateTime(2025, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "C# Session 1" },
                    { 4, 4, new DateTime(2025, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room 104", new DateTime(2025, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "UI/UX Session 1" },
                    { 5, 5, new DateTime(2025, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room 105", new DateTime(2025, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Business Session 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId_CourseId",
                table: "Grades",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CourseId",
                table: "Sessions",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
