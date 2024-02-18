import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { RoomIssueRoutingModule } from './room-issue-routing.module';
import { RoomIssueComponent } from './room-issue.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    RoomIssueComponent
  ],
  imports: [
    CommonModule,
    RoomIssueRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class RoomIssueModule { }
