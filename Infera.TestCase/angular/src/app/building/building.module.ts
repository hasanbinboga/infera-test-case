import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BuildingRoutingModule } from './building-routing.module';
import { BuildingComponent } from './building.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    BuildingComponent
  ],
  imports: [
    CommonModule,
    BuildingRoutingModule,
    SharedModule
  ]
})
export class BuildingModule { }
