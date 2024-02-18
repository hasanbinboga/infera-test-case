import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseInventoryIssueComponent } from './warehouse-inventory-issue.component';

const routes: Routes = [{ path: '', component: WarehouseInventoryIssueComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseInventoryIssueRoutingModule { }
