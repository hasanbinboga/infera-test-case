import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListService } from '@abp/ng.core';
import { BuildingRoutingModule } from './building-routing.module';
import { BuildingComponent } from './building.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    BuildingComponent
  ],
  imports: [
    CommonModule,
    BuildingRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ],
  providers: [
    ListService,
  ]
})
export class BuildingModule { }
