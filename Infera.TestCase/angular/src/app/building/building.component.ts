import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { BuildingService, BuildingDto } from '@proxy/buildings';

@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrl: './building.component.scss'
})
export class BuildingComponent implements OnInit {
  
  building = { items: [], totalCount: 0 } as PagedResultDto<BuildingDto>;
 
  constructor(public readonly list: ListService, private buildingService: BuildingService) {}

  ngOnInit(): void {
    const buildingStreamCreator = (query) => this.buildingService.getList(query);

    this.list.hookToQuery(buildingStreamCreator).subscribe((response) => {
      this.building = response;
    });
  }

}
