import type { InventoryCreateUpdateDto, InventoryDto, InventoryLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class WarehouseInventoryService {
  apiName = 'Default';
  

  create = (input: InventoryCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InventoryDto>({
      method: 'POST',
      url: '/api/app/warehouse-inventory',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/warehouse-inventory/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InventoryDto>({
      method: 'GET',
      url: `/api/app/warehouse-inventory/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getInventoryLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<InventoryLookupDto>>({
      method: 'GET',
      url: '/api/app/warehouse-inventory/inventory-lookup',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<InventoryDto>>({
      method: 'GET',
      url: '/api/app/warehouse-inventory',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: InventoryCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InventoryDto>({
      method: 'PUT',
      url: `/api/app/warehouse-inventory/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
