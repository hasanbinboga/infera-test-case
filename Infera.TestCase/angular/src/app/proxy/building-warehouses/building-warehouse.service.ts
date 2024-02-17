import type { BuildingWarehouseCreateUpdateDto, BuildingWarehouseDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BuildingWarehouseService {
  apiName = 'Default';
  

  create = (input: BuildingWarehouseCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingWarehouseDto>({
      method: 'POST',
      url: '/api/app/building-warehouse',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/building-warehouse/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingWarehouseDto>({
      method: 'GET',
      url: `/api/app/building-warehouse/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BuildingWarehouseDto>>({
      method: 'GET',
      url: '/api/app/building-warehouse',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: BuildingWarehouseCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BuildingWarehouseDto>({
      method: 'PUT',
      url: `/api/app/building-warehouse/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
