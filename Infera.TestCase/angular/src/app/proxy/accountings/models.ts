import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { AccountingType } from '../accounting-type.enum';
import type { ProductInventoryType } from '../product-inventory-type.enum';

export interface AccountingCreateUpdateDto extends EntityDto<string> {
  productInventoryId: string;
  saleOrderId?: string;
  count: number;
  purchasePrice: number;
  salePrice: number;
  amount: number;
  tax: number;
  invoiceDate?: string;
  invoiceNumber?: string;
  type: AccountingType;
}

export interface AccountingDto extends AuditedEntityDto<string> {
  productInventoryId?: string;
  productName?: string;
  productManifacturer?: string;
  productType: ProductInventoryType;
  productSize: number;
  saleOrderId?: string;
  roomBuildingId?: string;
  roomBuildingName?: string;
  roomId?: string;
  roomFloor?: number;
  roomNo?: string;
  warehouseInventoryId?: string;
  warehouseName?: string;
  warehouseNo?: string;
  warehouseFloor?: number;
  isOrderCompleted?: boolean;
  count: number;
  purchasePrice: number;
  salePrice: number;
  amount: number;
  tax: number;
  invoiceDate?: string;
  invoiceNumber?: string;
  type: AccountingType;
}
