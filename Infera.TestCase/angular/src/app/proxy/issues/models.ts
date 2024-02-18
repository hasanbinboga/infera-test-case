import type { AuditedEntityDto, EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { IssueType } from '../issue-type.enum';
import type { IssueEntityType } from '../issue-entity-type.enum';

export interface IssueCreateUpdateDto extends EntityDto<string> {
  buildingId?: string;
  roomId?: string;
  warehouseInventoryId?: string;
  productInventoryId?: string;
  assignee?: string;
  number?: number;
  type: IssueType;
  entityType: IssueEntityType;
  notes?: string;
  isCompleted?: boolean;
  completedTime?: string;
  completedTime1?: string;
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
  number?: number;
  type: IssueType;
  entityType: IssueEntityType;
  isCompleted?: boolean;
  completedTime?: string;
  notes?: string;
  assigneeId?: string;
  assigneeName?: string;
}

export interface IssueListFilterDto extends PagedAndSortedResultRequestDto {
  entityType: IssueEntityType;
  buildingId?: string;
  roomId?: string;
  warehouseInventoryId?: string;
  productInventoryId?: string;
}

export interface IssueLookupDto extends EntityDto<string> {
  name?: string;
}

export interface UserLookupDto extends EntityDto<string> {
  name?: string;
}
