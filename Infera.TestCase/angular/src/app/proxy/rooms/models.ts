import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';

export interface RoomCreateUpdateDto extends EntityDto<string> {
  buildingId: string;
  no: string;
  floor: number;
  capacity?: number;
  hasMiniBar?: boolean;
  notes?: string;
}

export interface RoomDto extends AuditedEntityDto<string> {
  buildingId?: string;
  buildingName?: string;
  floor: number;
  no?: string;
  capacity?: number;
  hasMiniBar?: boolean;
  notes?: string;
  saleOrderCount?: number;
  issueCount?: number;
}

export interface RoomLookupDto extends EntityDto<string> {
  name?: string;
}
