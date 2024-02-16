import { mapEnumToOptions } from '@abp/ng.core';

export enum ProductInventoryType {
  None = 0,
  Drink = 1,
  Snack = 2,
  Chocolate = 3,
  Alcohol = 4,
}

export const productInventoryTypeOptions = mapEnumToOptions(ProductInventoryType);
