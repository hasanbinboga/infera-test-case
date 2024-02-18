import type { IssueCreateUpdateDto, IssueDto, IssueListFilterDto, IssueLookupDto, UserLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class IssueService {
  apiName = 'Default';
  

  create = (input: IssueCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IssueDto>({
      method: 'POST',
      url: '/api/app/issue',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/issue/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IssueDto>({
      method: 'GET',
      url: `/api/app/issue/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getIssueLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<IssueLookupDto>>({
      method: 'GET',
      url: '/api/app/issue/issue-lookup',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<IssueDto>>({
      method: 'GET',
      url: '/api/app/issue',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListByEntityType = (input: IssueListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<IssueDto>>({
      method: 'GET',
      url: '/api/app/issue/by-entity-type',
      params: { entityType: input.entityType, buildingId: input.buildingId, roomId: input.roomId, warehouseInventoryId: input.warehouseInventoryId, productInventoryId: input.productInventoryId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getUserLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UserLookupDto>>({
      method: 'GET',
      url: '/api/app/issue/user-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: IssueCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IssueDto>({
      method: 'PUT',
      url: `/api/app/issue/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
