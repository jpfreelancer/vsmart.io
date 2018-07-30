using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartAdmin.Seed.Data.Migrations
{
    public partial class TopicEntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_SelectedSources",
                table: "Topics",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "_Keywords",
                table: "Topics",
                newName: "SelectedSources");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Topics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Topics");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Topics",
                newName: "_SelectedSources");

            migrationBuilder.RenameColumn(
                name: "SelectedSources",
                table: "Topics",
                newName: "_Keywords");
        }
    }
}
