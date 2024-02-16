import type { BuildingCreateUpdateDto, BuildingDto, BuildingLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BuildingService {
  apiName = 'Default';
  

  create = (input: BuildingCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingDto>({
      method: 'POST',
      url: '/api/app/building',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/building/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingDto>({
      method: 'GET',
      url: `/api/app/building/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getBuildingLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<BuildingLookupDto>>({
      method: 'GET',
      url: '/api/app/building/building-lookup',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BuildingDto>>({
      method: 'GET',
      url: '/api/app/building',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: BuildingCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingDto>({
      method: 'PUT',
      url: `/api/app/building/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
