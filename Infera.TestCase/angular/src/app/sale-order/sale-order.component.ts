import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { RoomLookupDto, RoomService } from '@proxy/rooms';
import { SaleOrderDto, SaleOrderService } from '@proxy/sale-orders';
import { InventoryLookupDto, WarehouseInventoryService } from '@proxy/warehouse-inventories';

@Component({
  selector: 'app-sale-order',
  templateUrl: './sale-order.component.html',
  styleUrl: './sale-order.component.scss'
})
export class SaleOrderComponent implements OnInit {
  saleOrder = { items: [], totalCount: 0 } as PagedResultDto<SaleOrderDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedSaleOrder= {} as SaleOrderDto;

  inventories: InventoryLookupDto[];
  rooms: RoomLookupDto[];
  
  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private saleOrderService: SaleOrderService,
    private inventoryService: WarehouseInventoryService,
    private roomService: RoomService,
    private confirmation: ConfirmationService) {
    
  }
  ngOnInit(): void {
    const saleOrderStreamCreator = (query) => {
      return this.saleOrderService.getList(query);
    };

    this.list.hookToQuery(saleOrderStreamCreator).subscribe((response) => {
      this.saleOrder = response;
    });

    this.inventoryService.getInventoryLookup().subscribe(s => {
      this.inventories = s.items;
    });

    this.roomService.getRoomLookup().subscribe(s => {
      this.rooms = s.items;
    });
  }

  create() {
    this.selectedSaleOrder = {} as SaleOrderDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      roomId: [this.selectedSaleOrder.roomId || null, Validators.required],
      warehouseInventoryId: [this.selectedSaleOrder.warehouseInventoryId || null, Validators.required],
      count: [this.selectedSaleOrder.count || null, Validators.required],   
      notes: [this.selectedSaleOrder.notes || "", Validators.maxLength(500)],
      isCompleted: [this.selectedSaleOrder.isCompleted || null, null]
    });
    
  }

  edit(id: string) {
    this.saleOrderService.get(id).subscribe((saleOrder) => {
      this.selectedSaleOrder = saleOrder;
      this.buildForm();
      this.isModalOpen = true;
    });
  }


  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedSaleOrder.id) {
      this.saleOrderService
        .update(this.selectedSaleOrder.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.saleOrderService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.saleOrderService.delete(id).subscribe(() => this.list.get());
      }
    });
  }



}
