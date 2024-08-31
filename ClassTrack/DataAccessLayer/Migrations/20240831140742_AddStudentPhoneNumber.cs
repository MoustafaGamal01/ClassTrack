using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttends_Students_StudentId",
                table: "StudentAttends");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentAttends",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttends_Students_StudentId",
                table: "StudentAttends",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttends_Students_StudentId",
                table: "StudentAttends");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentAttends",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttends_Students_StudentId",
                table: "StudentAttends",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
