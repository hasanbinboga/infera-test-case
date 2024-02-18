import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { ProductInventoryType } from '../product-inventory-type.enum';

export interface InventoryCreateUpdateDto extends EntityDto<string> {
  warehouseId: string;
  productId: string;
  count: number;
  capacity: number;
  notes?: string;
}

export interface InventoryDto extends AuditedEntityDto<string> {
  warehouseId?: string;
  warehouseName?: string;
  warehouseNo?: string;
  warehouseFloor?: number;
  warehouseCapacity?: number;
  warehouseNotes?: string;
  productId?: string;
  productName?: string;
  productManicaturer?: string;
  productType: ProductInventoryType;
  productSize: number;
  salePrice: number;
  productNotes?: string;
  count: number;
  capacity: number;
  notes?: string;
  saleCount: number;
  issueCount: number;
}

export interface InventoryLookupDto extends EntityDto<string> {
  name?: string;
}
