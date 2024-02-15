using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infera.TestCase.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppWarehouses_BuildingId",
                table: "AppWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_AppWarehouseInventories_WarehouseId",
                table: "AppWarehouseInventories");

            migrationBuilder.DropIndex(
                name: "IX_AppSaleOrders_RoomId",
                table: "AppSaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppRooms_BuildingId",
                table: "AppRooms");

            migrationBuilder.DropIndex(
                name: "IX_AppIssues_BuildingId",
                table: "AppIssues");

            migrationBuilder.DropIndex(
                name: "IX_AppBuildingWarehouses_BuildingId",
                table: "AppBuildingWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_AppAccountings_ProductInventoryId",
                table: "AppAccountings");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "AppWarehouses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "AppAccountings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouses_BuildingId_Name_No_Floor_Capacity_IsDeleted",
                table: "AppWarehouses",
                columns: new[] { "BuildingId", "Name", "No", "Floor", "Capacity", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouseInventories_WarehouseId_ProductInventoryId_Count_Capacity_IsDeleted",
                table: "AppWarehouseInventories",
                columns: new[] { "WarehouseId", "ProductInventoryId", "Count", "Capacity", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppSaleOrders_RoomId_WarehouseInventoryId_Count_IsCompleted_IsDeleted",
                table: "AppSaleOrders",
                columns: new[] { "RoomId", "WarehouseInventoryId", "Count", "IsCompleted", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppRooms_BuildingId_No_Floor_HasMiniBar_Capacity_IsDeleted",
                table: "AppRooms",
                columns: new[] { "BuildingId", "No", "Floor", "HasMiniBar", "Capacity", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppProductInventories_Name_Manifacturer_Type_Size_SalePrice_IsDeleted",
                table: "AppProductInventories",
                columns: new[] { "Name", "Manifacturer", "Type", "Size", "SalePrice", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppIssues_BuildingId_RoomId_WarehouseInventoryId_ProductInventoryId_Type_Assignee_IsCompleted_CompletedTime_IsDeleted",
                table: "AppIssues",
                columns: new[] { "BuildingId", "RoomId", "WarehouseInventoryId", "ProductInventoryId", "Type", "Assignee", "IsCompleted", "CompletedTime", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBuildingWarehouses_BuildingId_WarehouseId_IsDeleted",
                table: "AppBuildingWarehouses",
                columns: new[] { "BuildingId", "WarehouseId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBuildings_Name_No_IsDeleted",
                table: "AppBuildings",
                columns: new[] { "Name", "No", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppAccountings_ProductInventoryId_SaleOrderId_Type_InvoiceDate_InvoiceNumber_SalePrice_PurchasePrice_IsDeleted",
                table: "AppAccountings",
                columns: new[] { "ProductInventoryId", "SaleOrderId", "Type", "InvoiceDate", "InvoiceNumber", "SalePrice", "PurchasePrice", "IsDeleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppWarehouses_BuildingId_Name_No_Floor_Capacity_IsDeleted",
                table: "AppWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_AppWarehouseInventories_WarehouseId_ProductInventoryId_Count_Capacity_IsDeleted",
                table: "AppWarehouseInventories");

            migrationBuilder.DropIndex(
                name: "IX_AppSaleOrders_RoomId_WarehouseInventoryId_Count_IsCompleted_IsDeleted",
                table: "AppSaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppRooms_BuildingId_No_Floor_HasMiniBar_Capacity_IsDeleted",
                table: "AppRooms");

            migrationBuilder.DropIndex(
                name: "IX_AppProductInventories_Name_Manifacturer_Type_Size_SalePrice_IsDeleted",
                table: "AppProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_AppIssues_BuildingId_RoomId_WarehouseInventoryId_ProductInventoryId_Type_Assignee_IsCompleted_CompletedTime_IsDeleted",
                table: "AppIssues");

            migrationBuilder.DropIndex(
                name: "IX_AppBuildingWarehouses_BuildingId_WarehouseId_IsDeleted",
                table: "AppBuildingWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_AppBuildings_Name_No_IsDeleted",
                table: "AppBuildings");

            migrationBuilder.DropIndex(
                name: "IX_AppAccountings_ProductInventoryId_SaleOrderId_Type_InvoiceDate_InvoiceNumber_SalePrice_PurchasePrice_IsDeleted",
                table: "AppAccountings");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "AppWarehouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "AppAccountings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouses_BuildingId",
                table: "AppWarehouses",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouseInventories_WarehouseId",
                table: "AppWarehouseInventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSaleOrders_RoomId",
                table: "AppSaleOrders",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRooms_BuildingId",
                table: "AppRooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AppIssues_BuildingId",
                table: "AppIssues",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBuildingWarehouses_BuildingId",
                table: "AppBuildingWarehouses",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AppAccountings_ProductInventoryId",
                table: "AppAccountings",
                column: "ProductInventoryId");
        }
    }
}
