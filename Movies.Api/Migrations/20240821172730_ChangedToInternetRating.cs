﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToInternetRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m
                );

            migrationBuilder.RenameColumn(
                name: "ImdbRating",
                table: "Pictures",
                newName: "InternetRating"
            );
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<int>(
                name: "InternetRating",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.RenameColumn(
                name: "InternetRating",
                table: "Pictures",
                newName: "ImdbRating"
            );
            
        }
    }
}
