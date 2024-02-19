import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  }, 
  { path: 'buildings', loadChildren: () => import('./building/building.module').then(m => m.BuildingModule) },
  { path: 'rooms', loadChildren: () => import('./room/room.module').then(m => m.RoomModule) },
  { path: 'warehouses', loadChildren: () => import('./warehouse/warehouse.module').then(m => m.WarehouseModule) },
  { path: 'products', loadChildren: () => import('./product/product.module').then(m => m.ProductModule) },
  { path: 'building-issues', loadChildren: () => import('./building-issue/building-issue.module').then(m => m.BuildingIssueModule) },
  { path: 'room-issues', loadChildren: () => import('./room-issue/room-issue.module').then(m => m.RoomIssueModule) },
  { path: 'warehouse-inventory-issues', loadChildren: () => import('./warehouse-inventory-issue/warehouse-inventory-issue.module').then(m => m.WarehouseInventoryIssueModule) },
  { path: 'product-issues', loadChildren: () => import('./product-issue/product-issue.module').then(m => m.ProductIssueModule) },
  { path: 'warehouse-inventories', loadChildren: () => import('./warehouse-inventory/warehouse-inventory.module').then(m => m.WarehouseInventoryModule) },
  { path: 'accounting', loadChildren: () => import('./accounting/accounting.module').then(m => m.AccountingModule) },
  { path: 'sale-orders', loadChildren: () => import('./sale-order/sale-order.module').then(m => m.SaleOrderModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
