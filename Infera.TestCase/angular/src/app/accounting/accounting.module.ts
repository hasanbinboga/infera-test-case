import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';


import { AccountingRoutingModule } from './accounting-routing.module';
import { AccountingComponent } from './accounting.component';


@NgModule({
  declarations: [
    AccountingComponent
  ],
  imports: [
    CommonModule,
    AccountingRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class AccountingModule { }
