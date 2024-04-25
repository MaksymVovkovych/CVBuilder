using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceSkillRefact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperiencesSkills",
                table: "ExperiencesSkills");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Resumes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExperiencesSkills",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExperiencesSkills",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ExperiencesSkills",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExperiencesSkills",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperiencesSkills",
                table: "ExperiencesSkills",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExperiencesSkills_SkillId",
                table: "ExperiencesSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperiencesSkills",
                table: "ExperiencesSkills");

            migrationBuilder.DropIndex(
                name: "IX_ExperiencesSkills_SkillId",
                table: "ExperiencesSkills");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExperiencesSkills");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExperiencesSkills");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ExperiencesSkills");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ExperiencesSkills");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Resumes",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperiencesSkills",
                table: "ExperiencesSkills",
                columns: new[] { "SkillId", "ExperienceId" });
        }
    }
}
