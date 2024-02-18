import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { WarehouseInventoryRoutingModule } from './warehouse-inventory-routing.module';
import { WarehouseInventoryComponent } from './warehouse-inventory.component';


@NgModule({
  declarations: [
    WarehouseInventoryComponent
  ],
  imports: [
    CommonModule,
    WarehouseInventoryRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService
  ]
})
export class WarehouseInventoryModule { }
