import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { SaleOrderRoutingModule } from './sale-order-routing.module';
import { SaleOrderComponent } from './sale-order.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    SaleOrderComponent
  ],
  imports: [
    CommonModule,
    SaleOrderRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class SaleOrderModule { }
