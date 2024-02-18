import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductIssueComponent } from './product-issue.component';

const routes: Routes = [{ path: '', component: ProductIssueComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductIssueRoutingModule { }
