import { mapEnumToOptions } from '@abp/ng.core';

export enum IssueEntityType {
  None = 0,
  Building = 1,
  Room = 2,
  WarehouseInventory = 3,
  ProductInventory = 4,
}

export const issueEntityTypeOptions = mapEnumToOptions(IssueEntityType);
