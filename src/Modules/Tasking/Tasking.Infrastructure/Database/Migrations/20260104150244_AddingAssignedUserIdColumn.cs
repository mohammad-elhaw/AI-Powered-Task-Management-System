using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasking.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssignedUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                schema: "tasking",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                schema: "tasking",
                table: "Tasks");
        }
    }
}
