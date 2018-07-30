using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartAdmin.Seed.Data.Migrations
{
    public partial class AddTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Articles",
                newName: "articlesID");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleContent = table.Column<string>(nullable: true),
                    ArticleDescription = table.Column<string>(nullable: true),
                    ArticleTitle = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArticleVersions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleContent = table.Column<string>(nullable: true),
                    ArticleDescription = table.Column<string>(nullable: true),
                    ArticleTitle = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    TasksID = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    VersionNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVersions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArticleVersions_Tasks_TasksID",
                        column: x => x.TasksID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TasksID = table.Column<int>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskHistory_Tasks_TasksID",
                        column: x => x.TasksID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVersions_TasksID",
                table: "ArticleVersions",
                column: "TasksID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_TasksID",
                table: "TaskHistory",
                column: "TasksID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVersions");

            migrationBuilder.DropTable(
                name: "TaskHistory");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "articlesID",
                table: "Articles",
                newName: "ID");
        }
    }
}
