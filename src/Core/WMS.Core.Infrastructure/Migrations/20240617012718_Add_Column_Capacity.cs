﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_Capacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Locations");
        }
    }
}
