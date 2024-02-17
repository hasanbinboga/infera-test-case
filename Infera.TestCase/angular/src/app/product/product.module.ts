import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';


@NgModule({
  declarations: [
    ProductComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class ProductModule { }
