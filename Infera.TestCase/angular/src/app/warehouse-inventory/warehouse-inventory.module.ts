import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseInventoryRoutingModule } from './warehouse-inventory-routing.module';
import { WarehouseInventoryComponent } from './warehouse-inventory.component';


@NgModule({
  declarations: [
    WarehouseInventoryComponent
  ],
  imports: [
    CommonModule,
    WarehouseInventoryRoutingModule
  ]
})
export class WarehouseInventoryModule { }
