import type { ProductInventoryCreateUpdateDto, ProductInventoryDto, ProductInventoryLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductInventoryService {
  apiName = 'Default';
  

  create = (input: ProductInventoryCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductInventoryDto>({
      method: 'POST',
      url: '/api/app/product-inventory',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/product-inventory/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductInventoryDto>({
      method: 'GET',
      url: `/api/app/product-inventory/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProductInventoryDto>>({
      method: 'GET',
      url: '/api/app/product-inventory',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getProductInventoryLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<ProductInventoryLookupDto>>({
      method: 'GET',
      url: '/api/app/product-inventory/product-inventory-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: ProductInventoryCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductInventoryDto>({
      method: 'PUT',
      url: `/api/app/product-inventory/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
