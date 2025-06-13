using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_oev_backend_rest.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_course_tbl_user_UserId",
                table: "tbl_course");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_course_tbl_user_UserId",
                table: "tbl_course",
                column: "UserId",
                principalTable: "tbl_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_course_tbl_user_UserId",
                table: "tbl_course");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_course_tbl_user_UserId",
                table: "tbl_course",
                column: "UserId",
                principalTable: "tbl_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
