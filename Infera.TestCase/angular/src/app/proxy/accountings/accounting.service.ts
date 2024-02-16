import type { AccountingCreateUpdateDto, AccountingDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountingService {
  apiName = 'Default';
  

  create = (input: AccountingCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountingDto>({
      method: 'POST',
      url: '/api/app/accounting',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/accounting/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountingDto>({
      method: 'GET',
      url: `/api/app/accounting/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<AccountingDto>>({
      method: 'GET',
      url: '/api/app/accounting',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: AccountingCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountingDto>({
      method: 'PUT',
      url: `/api/app/accounting/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
