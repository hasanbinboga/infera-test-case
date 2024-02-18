import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseInventoryIssueRoutingModule } from './warehouse-inventory-issue-routing.module';
import { WarehouseInventoryIssueComponent } from './warehouse-inventory-issue.component';


@NgModule({
  declarations: [
    WarehouseInventoryIssueComponent
  ],
  imports: [
    CommonModule,
    WarehouseInventoryIssueRoutingModule
  ]
})
export class WarehouseInventoryIssueModule { }
