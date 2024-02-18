import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingIssueComponent } from './building-issue.component';

const routes: Routes = [{ path: '', component: BuildingIssueComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BuildingIssueRoutingModule { }
