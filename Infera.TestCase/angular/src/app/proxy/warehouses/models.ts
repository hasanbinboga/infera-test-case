import type { AuditedEntityDto, EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BuildingWarehouseDto extends AuditedEntityDto<string> {
  relatedBuildingId?: string;
  warehouseId?: string;
  buildingId?: string;
  buildingName?: string;
  name?: string;
  no?: string;
  floor: number;
  capacity?: number;
  content?: string;
  notes?: string;
  buildingCount?: number;
  inventoryCount?: number;
}

export interface WarehouseCreateUpdateDto extends EntityDto<string> {
  buildingId?: string;
  name: string;
  no: string;
  floor: number;
  capacity: number;
  content?: string;
  notes?: string;
}

export interface WarehouseDto extends AuditedEntityDto<string> {
  buildingId?: string;
  buildingName?: string;
  name?: string;
  no?: string;
  floor: number;
  capacity?: number;
  content?: string;
  notes?: string;
  buildingCount?: number;
  inventoryCount?: number;
}

export interface WarehouseListFilterDto extends PagedAndSortedResultRequestDto {
  warehouseId?: string;
}

export interface WarehouseLookupDto extends EntityDto<string> {
  name?: string;
}
