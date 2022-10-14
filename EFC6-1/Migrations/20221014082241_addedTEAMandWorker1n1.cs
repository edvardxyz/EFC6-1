using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFC6_1.Migrations
{
    public partial class addedTEAMandWorker1n1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTodoTodoId",
                table: "Workers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentTaskTaskId",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CurrentTodoTodoId",
                table: "Workers",
                column: "CurrentTodoTodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTaskTaskId",
                table: "Teams",
                column: "CurrentTaskTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskTaskId",
                table: "Teams",
                column: "CurrentTaskTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Todos_CurrentTodoTodoId",
                table: "Workers",
                column: "CurrentTodoTodoId",
                principalTable: "Todos",
                principalColumn: "TodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskTaskId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Todos_CurrentTodoTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_CurrentTodoTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentTaskTaskId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CurrentTodoTodoId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CurrentTaskTaskId",
                table: "Teams");
        }
    }
}
