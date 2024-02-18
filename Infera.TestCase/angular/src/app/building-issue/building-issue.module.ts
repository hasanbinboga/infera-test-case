import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { BuildingIssueRoutingModule } from './building-issue-routing.module';
import { BuildingIssueComponent } from './building-issue.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    BuildingIssueComponent
  ],
  imports: [
    CommonModule,
    BuildingIssueRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class BuildingIssueModule { }
