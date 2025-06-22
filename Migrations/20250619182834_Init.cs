using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <summary>
    /// Initial migration that sets up the database schema
    /// </summary>
    public partial class Init : Migration
    {
        /// <summary>
        /// This method runs when applying the migration (e.g., when using `update-database`)
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create a new table called "Users"
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    // Primary key with auto-increment
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Full name of the user
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    // Email of the user
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    // Encrypted password (hashed)
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    // Role of the user (e.g., Admin or User)
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    // Define Id as the primary key
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <summary>
        /// This method reverses the migration (e.g., if rolling back)
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the "Users" table
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
