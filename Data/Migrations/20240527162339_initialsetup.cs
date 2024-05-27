using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFinder2._0.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFound = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUderID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pets_AspNetUsers_IdentityUserID",
                        column: x => x.IdentityUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityUderID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_IdentityUserID",
                        column: x => x.IdentityUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Pets_PetID",
                        column: x => x.PetID,
                        principalTable: "Pets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdentityUserID",
                table: "Comments",
                column: "IdentityUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PetID",
                table: "Comments",
                column: "PetID");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_IdentityUserID",
                table: "Pets",
                column: "IdentityUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
