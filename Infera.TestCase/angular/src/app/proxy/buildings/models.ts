import type { AuditedEntityDto, EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BuildingCreateUpdateDto extends EntityDto<string> {
  name: string;
  no: string;
  addres?: string;
}

export interface BuildingDto extends AuditedEntityDto<string> {
  name?: string;
  no?: string;
  addres?: string;
  roomCount?: number;
  inWarehouseCount?: number;
  ownWarehouseCount?: number;
  issueCount?: number;
}

export interface BuildingListFilterDto extends PagedAndSortedResultRequestDto {
  warehouseId?: string;
}

export interface BuildingLookupDto extends EntityDto<string> {
  name?: string;
}

export interface BuildingWarehouseDto extends AuditedEntityDto<string> {
  buildingId?: string;
  warehouseId?: string;
  name?: string;
  no?: string;
  addres?: string;
  roomCount?: number;
  inWarehouseCount?: number;
  ownWarehouseCount?: number;
  issueCount?: number;
}
