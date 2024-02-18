import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoomIssueRoutingModule } from './room-issue-routing.module';
import { RoomIssueComponent } from './room-issue.component';


@NgModule({
  declarations: [
    RoomIssueComponent
  ],
  imports: [
    CommonModule,
    RoomIssueRoutingModule
  ]
})
export class RoomIssueModule { }
