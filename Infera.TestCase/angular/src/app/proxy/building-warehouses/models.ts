import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';

export interface BuildingWarehouseCreateUpdateDto extends EntityDto<string> {
  buildingId: string;
  warehouseId: string;
}

export interface BuildingWarehouseDto extends AuditedEntityDto<string> {
  buildingId?: string;
  warehouseId?: string;
}
