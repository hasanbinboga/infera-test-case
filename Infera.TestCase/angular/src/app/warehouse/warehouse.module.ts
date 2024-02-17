import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { WarehouseRoutingModule } from './warehouse-routing.module';
import { WarehouseComponent } from './warehouse.component';


@NgModule({
  declarations: [
    WarehouseComponent
  ],
  imports: [
    CommonModule,
    WarehouseRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class WarehouseModule { }
