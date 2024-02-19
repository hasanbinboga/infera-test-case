import type { SaleOrderCreateUpdateDto, SaleOrderDto, SaleOrderLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SaleOrderService {
  apiName = 'Default';
  

  create = (input: SaleOrderCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SaleOrderDto>({
      method: 'POST',
      url: '/api/app/sale-order',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/sale-order/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SaleOrderDto>({
      method: 'GET',
      url: `/api/app/sale-order/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SaleOrderDto>>({
      method: 'GET',
      url: '/api/app/sale-order',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSaleOrderLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<SaleOrderLookupDto>>({
      method: 'GET',
      url: '/api/app/sale-order/sale-order-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: SaleOrderCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SaleOrderDto>({
      method: 'PUT',
      url: `/api/app/sale-order/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
