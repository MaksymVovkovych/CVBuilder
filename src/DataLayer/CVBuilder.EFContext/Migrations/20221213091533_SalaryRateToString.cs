using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    /// <inheritdoc />
    public partial class SalaryRateToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SalaryRate",
                table: "Resumes",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,2)",
                oldPrecision: 9,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryRate",
                table: "Resumes",
                type: "numeric(9,2)",
                precision: 9,
                scale: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
