import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SaleOrderRoutingModule } from './sale-order-routing.module';
import { SaleOrderComponent } from './sale-order.component';


@NgModule({
  declarations: [
    SaleOrderComponent
  ],
  imports: [
    CommonModule,
    SaleOrderRoutingModule
  ]
})
export class SaleOrderModule { }
