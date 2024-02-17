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
        order: 4,
        layout: eLayoutType.application,
      },
    ]);
  };
}
