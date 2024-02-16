import { mapEnumToOptions } from '@abp/ng.core';

export enum IssueType {
  None = 0,
  Cleaning = 1,
  Fix = 2,
  Refill = 3,
}

export const issueTypeOptions = mapEnumToOptions(IssueType);
