import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { ProductInventoryType } from '../product-inventory-type.enum';

export interface SaleOrderCreateUpdateDto extends EntityDto<string> {
  roomId: string;
  warehouseInventoryId: string;
  count: number;
  isCompleted: boolean;
  notes?: string;
}

export interface SaleOrderDto extends AuditedEntityDto<string> {
  roomId?: string;
  roomBuildingId?: string;
  roomBuildingName?: string;
  roomFloor: number;
  roomNo?: string;
  warehouseInventoryId?: string;
  warehouseId?: string;
  warehouseName?: string;
  warehouseNo?: string;
  warehouseFloor?: number;
  warehouseCapacity?: number;
  productId?: string;
  productName?: string;
  productManicaturer?: string;
  productType: ProductInventoryType;
  productSize: number;
  salePrice: number;
  count: number;
  isCompleted: boolean;
  notes?: string;
}

export interface SaleOrderLookupDto extends EntityDto<string> {
  name?: string;
}
