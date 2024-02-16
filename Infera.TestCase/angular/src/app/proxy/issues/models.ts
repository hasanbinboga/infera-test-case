import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { IssueType } from '../issue-type.enum';

export interface IssueCreateUpdateDto extends EntityDto<string> {
  buildingId?: string;
  roomId?: string;
  warehouseInventoryId?: string;
  productInventoryId?: string;
  assignee?: string;
  number: number;
  type: IssueType;
  notes?: string;
  isCompleted: boolean;
  completedTime?: string;
}

export interface IssueDto extends AuditedEntityDto<string> {
  buildingId?: string;
  buildingName?: string;
  roomBuildingId?: string;
  roomBuildingName?: string;
  roomId?: string;
  roomFloor?: number;
  roomNo?: string;
  roomCapacity?: number;
  warehouseInventoryId?: string;
  warehouseName?: string;
  warehouseNo?: string;
  warehouseFloor?: number;
  productInventoryId?: string;
  productName?: string;
  number: number;
  type: IssueType;
  isCompleted: boolean;
  completedTime?: string;
  notes?: string;
  assignee?: string;
}

export interface IssueLookupDto extends EntityDto<string> {
  name?: string;
}
