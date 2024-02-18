import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomIssueComponent } from './room-issue.component';

const routes: Routes = [{ path: '', component: RoomIssueComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoomIssueRoutingModule { }
