using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartAdmin.Seed.Data.Migrations
{
    public partial class TopicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_IndexViewModel_IndexViewModelID",
                table: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "IndexViewModel");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_IndexViewModelID",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "IndexViewModelID",
                table: "AspNetUserRoles");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SourcesID = table.Column<int>(nullable: true),
                    _Tags = table.Column<string>(nullable: true),
                    articleContent = table.Column<string>(nullable: true),
                    articleDescription = table.Column<string>(nullable: true),
                    articleTitle = table.Column<string>(nullable: false),
                    articleUrl = table.Column<string>(nullable: true),
                    lastModified = table.Column<DateTime>(nullable: false),
                    publishedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Articles_Sources_SourcesID",
                        column: x => x.SourcesID,
                        principalTable: "Sources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    TopicName = table.Column<string>(nullable: true),
                    _Keywords = table.Column<string>(nullable: true),
                    _SelectedSources = table.Column<string>(nullable: true),
                    isFeatured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SourcesID",
                table: "Articles",
                column: "SourcesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.AddColumn<string>(
                name: "IndexViewModelID",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndexViewModel",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    StatusMessage = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexViewModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastModified = table.Column<DateTime>(nullable: false),
                    PostContent = table.Column<string>(nullable: true),
                    PostDescription = table.Column<string>(nullable: true),
                    PostTitle = table.Column<string>(nullable: false),
                    PublishedDate = table.Column<DateTime>(nullable: false),
                    SourcesID = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    _Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_Sources_SourcesID",
                        column: x => x.SourcesID,
                        principalTable: "Sources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_IndexViewModelID",
                table: "AspNetUserRoles",
                column: "IndexViewModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SourcesID",
                table: "Posts",
                column: "SourcesID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_IndexViewModel_IndexViewModelID",
                table: "AspNetUserRoles",
                column: "IndexViewModelID",
                principalTable: "IndexViewModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
