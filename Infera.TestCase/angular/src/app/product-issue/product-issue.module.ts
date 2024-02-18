import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductIssueRoutingModule } from './product-issue-routing.module';
import { ProductIssueComponent } from './product-issue.component';


@NgModule({
  declarations: [
    ProductIssueComponent
  ],
  imports: [
    CommonModule,
    ProductIssueRoutingModule
  ]
})
export class ProductIssueModule { }
