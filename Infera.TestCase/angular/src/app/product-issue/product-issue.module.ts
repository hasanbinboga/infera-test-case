import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { ProductIssueRoutingModule } from './product-issue-routing.module';
import { ProductIssueComponent } from './product-issue.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    ProductIssueComponent
  ],
  imports: [
    CommonModule,
    ProductIssueRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class ProductIssueModule { }
