import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { ProductInventoryType } from '../product-inventory-type.enum';

export interface ProductInventoryCreateUpdateDto extends EntityDto<string> {
  name: string;
  manifacturer: string;
  type: ProductInventoryType;
  size: number;
  salePrice: number;
  notes?: string;
}

export interface ProductInventoryDto extends AuditedEntityDto<string> {
  name?: string;
  manifacturer?: string;
  type: ProductInventoryType;
  size: number;
  salePrice: number;
  notes?: string;
  warehouseInventoryCount?: number;
  accountingCount?: number;
  issueCount?: number;
}

export interface ProductInventoryLookupDto extends EntityDto<string> {
  name?: string;
}
