import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { WarehouseInventoryIssueRoutingModule } from './warehouse-inventory-issue-routing.module';
import { WarehouseInventoryIssueComponent } from './warehouse-inventory-issue.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    WarehouseInventoryIssueComponent
  ],
  imports: [
    CommonModule,
    WarehouseInventoryIssueRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class WarehouseInventoryIssueModule { }
