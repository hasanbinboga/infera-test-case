import { mapEnumToOptions } from '@abp/ng.core';

export enum AccountingType {
  None = 0,
  Input = 1,
  Output = 2,
}

export const accountingTypeOptions = mapEnumToOptions(AccountingType);
