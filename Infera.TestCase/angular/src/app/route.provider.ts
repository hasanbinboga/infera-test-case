import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/buildings',
        name: '::Menu:Buildings',
        iconClass: 'fas fa-building',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/rooms',
        name: '::Menu:Rooms',
        iconClass: 'fas fa-person-booth',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/warehouses',
        name: '::Menu:Warehouses',
        iconClass: 'fas fa-warehouse',
        order: 4,
        layout: eLayoutType.application,
      },
      {
        path: '/products',
        name: '::Menu:Products',
        iconClass: 'fas fa-cookie-bite',
        order: 5,
        layout: eLayoutType.application,
      },
      {
        path: '/warehouse-inventories',
        name: '::Menu:WarehouseInventories',
        iconClass: 'fas fa-store',
        order: 6,
        layout: eLayoutType.application,
      },
      {
        path: '/issues',
        name: '::Menu:Issues',
        iconClass: 'fas fa-check',
        order: 7,
        layout: eLayoutType.application,
      },
      {
        path: '/building-issues',
        name: '::Menu:BuildingIssues',
        iconClass: 'fas fa-file',
        parentName: '::Menu:Issues',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/room-issues',
        name: '::Menu:RoomIssues',
        iconClass: 'fas fa-tasks',
        parentName: '::Menu:Issues',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/warehouse-inventory-issues',
        name: '::Menu:WarehouseInventoryIssues',
        iconClass: 'fas fa-boxes',
        parentName: '::Menu:Issues',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/product-issues',
        name: '::Menu:ProductIssues',
        iconClass: 'fas fa-dolly',
        parentName: '::Menu:Issues',
        order: 4,
        layout: eLayoutType.application,
      },
    ]);
  };
}
