using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageAndShoppingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_AppUsers_AddedByUserId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_StorageGroups_GroupId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdItems_AppUsers_AddedByUserId",
                table: "HouseholdItems");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdItems_StorageGroups_GroupId",
                table: "HouseholdItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItems_StorageGroups_GroupId",
                table: "ShoppingListItems");

            migrationBuilder.DropIndex(
                name: "IX_StorageGroups_AdminUserId",
                table: "StorageGroups");

            migrationBuilder.DropIndex(
                name: "IX_HouseholdItems_AddedByUserId",
                table: "HouseholdItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_AddedByUserId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "HouseholdItems");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "FoodItems");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ShoppingListItems",
                newName: "ShoppingListId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListItems_GroupId",
                table: "ShoppingListItems",
                newName: "IX_ShoppingListItems_ShoppingListId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "HouseholdItems",
                newName: "StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_HouseholdItems_GroupId",
                table: "HouseholdItems",
                newName: "IX_HouseholdItems_StorageId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "FoodItems",
                newName: "StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_GroupId",
                table: "FoodItems",
                newName: "IX_FoodItems_StorageId");

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_StorageGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "StorageGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storages_StorageGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "StorageGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageGroups_AdminUserId",
                table: "StorageGroups",
                column: "AdminUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_AppUserId",
                table: "ShoppingLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_GroupId",
                table: "ShoppingLists",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_AppUserId",
                table: "Storages",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storages_GroupId",
                table: "Storages",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Storages_StorageId",
                table: "FoodItems",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdItems_Storages_StorageId",
                table: "HouseholdItems",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                table: "ShoppingListItems",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Storages_StorageId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdItems_Storages_StorageId",
                table: "HouseholdItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                table: "ShoppingListItems");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_StorageGroups_AdminUserId",
                table: "StorageGroups");

            migrationBuilder.RenameColumn(
                name: "ShoppingListId",
                table: "ShoppingListItems",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListItems_ShoppingListId",
                table: "ShoppingListItems",
                newName: "IX_ShoppingListItems_GroupId");

            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "HouseholdItems",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_HouseholdItems_StorageId",
                table: "HouseholdItems",
                newName: "IX_HouseholdItems_GroupId");

            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "FoodItems",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_StorageId",
                table: "FoodItems",
                newName: "IX_FoodItems_GroupId");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "HouseholdItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "FoodItems",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageGroups_AdminUserId",
                table: "StorageGroups",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdItems_AddedByUserId",
                table: "HouseholdItems",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_AddedByUserId",
                table: "FoodItems",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_AppUsers_AddedByUserId",
                table: "FoodItems",
                column: "AddedByUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_StorageGroups_GroupId",
                table: "FoodItems",
                column: "GroupId",
                principalTable: "StorageGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdItems_AppUsers_AddedByUserId",
                table: "HouseholdItems",
                column: "AddedByUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdItems_StorageGroups_GroupId",
                table: "HouseholdItems",
                column: "GroupId",
                principalTable: "StorageGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItems_StorageGroups_GroupId",
                table: "ShoppingListItems",
                column: "GroupId",
                principalTable: "StorageGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
