using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class InheritanceStarted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_Actors");

            migrationBuilder.DropTable(
                name: "Movie_Directors");

            migrationBuilder.AddColumn<string>(
                name: "ChannelFirstAiredOn",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pictures",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossRevenue",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelFirstAiredOn",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "GrossRevenue",
                table: "Pictures");

            migrationBuilder.CreateTable(
                name: "Movie_Actors",
                columns: table => new
                {
                    MovieIdentifier = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Actors", x => new { x.MovieIdentifier, x.Id });
                    table.ForeignKey(
                        name: "FK_Movie_Actors_Pictures_MovieIdentifier",
                        column: x => x.MovieIdentifier,
                        principalTable: "Pictures",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movie_Directors",
                columns: table => new
                {
                    MovieIdentifier = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Directors", x => x.MovieIdentifier);
                    table.ForeignKey(
                        name: "FK_Movie_Directors_Pictures_MovieIdentifier",
                        column: x => x.MovieIdentifier,
                        principalTable: "Pictures",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
