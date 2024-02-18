import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseInventoryComponent } from './warehouse-inventory.component';

const routes: Routes = [{ path: '', component: WarehouseInventoryComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseInventoryRoutingModule { }
