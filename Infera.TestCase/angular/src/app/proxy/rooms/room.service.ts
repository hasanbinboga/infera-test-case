import type { RoomCreateUpdateDto, RoomDto, RoomLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  apiName = 'Default';
  

  create = (input: RoomCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RoomDto>({
      method: 'POST',
      url: '/api/app/room',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/room/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RoomDto>({
      method: 'GET',
      url: `/api/app/room/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<RoomDto>>({
      method: 'GET',
      url: '/api/app/room',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getRoomLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<RoomLookupDto>>({
      method: 'GET',
      url: '/api/app/room/room-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: RoomCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RoomDto>({
      method: 'PUT',
      url: `/api/app/room/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
