using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EncouterValours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antedecents",
                columns: table => new
                {
                    AntedecentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antedecents", x => x.AntedecentId);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    EncounterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PateientId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: false),
                    Reasons = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    Subjective = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Objetive = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.EncounterId);
                });

            migrationBuilder.CreateTable(
                name: "Attchment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attchment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attchment_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "EncounterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Encounters",
                columns: new[] { "EncounterId", "AppointmentId", "Assessment", "CreatedAt", "Date", "DoctorId", "Notes", "Objetive", "PateientId", "Plan", "Reasons", "Status", "Subjective", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1L, "El dolor fue generado por aire que se concentro en la espalda", new DateTimeOffset(new DateTime(2025, 8, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "Ninguna", "El dolor es generado por aire", 1L, "Pasar azufre sobre la zona afectada", "Dolor de espalda", "Open", "Dolor intenso arriba a un costado de la espalda", new DateTimeOffset(new DateTime(2025, 8, 20, 8, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2L, "El dolor de cabeza debido a fiebre. El paciente tiene una temperatura de 38°", new DateTimeOffset(new DateTime(2025, 9, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2025, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "Ninguna", "Resfrio", 4L, "reposo y tomar antigripal cada 12hs", "Dolor de cabeza", "Signed", "Dolor de cabeza", new DateTimeOffset(new DateTime(2025, 9, 11, 10, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 3L, "El paciente se contagio de un resfriado", new DateTimeOffset(new DateTime(2025, 10, 15, 15, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Ninguna", "Resfrio", 2L, "Resposo, tomar un antigripal cada 8hs y realizar vapores para sacar los mocos", "Tos y mocos", "Signed", "Tos fuerte y acumulacion de mocos", new DateTimeOffset(new DateTime(2025, 10, 15, 15, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attchment_EncounterId",
                table: "Attchment",
                column: "EncounterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antedecents");

            migrationBuilder.DropTable(
                name: "Attchment");

            migrationBuilder.DropTable(
                name: "Encounters");
        }
    }
}
