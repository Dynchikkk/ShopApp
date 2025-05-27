using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_AuthUsers", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Products", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_UserProfiles_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_CartItems", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_CartItems_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_CartItems_AuthUserId",
                table: "CartItems",
                column: "AuthUserId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AuthUserId",
                table: "UserProfiles",
                column: "AuthUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "CartItems");

            _ = migrationBuilder.DropTable(
                name: "UserProfiles");

            _ = migrationBuilder.DropTable(
                name: "Products");

            _ = migrationBuilder.DropTable(
                name: "AuthUsers");
        }
    }
}
