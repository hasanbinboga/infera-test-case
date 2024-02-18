import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueEntityTypeOptions, issueTypeOptions } from '@proxy';
import { IssueDto, IssueListFilterDto, IssueService, UserLookupDto } from '@proxy/issues';
import { ProductInventoryLookupDto, ProductInventoryService } from '@proxy/product-inventories';
import { InventoryDto, WarehouseInventoryService } from '@proxy/warehouse-inventories';
import { WarehouseLookupDto, WarehouseService } from '@proxy/warehouses';


@Component({
  selector: 'app-warehouse-inventory',
  templateUrl: './warehouse-inventory.component.html',
  styleUrl: './warehouse-inventory.component.scss'
})
export class WarehouseInventoryComponent implements OnInit {

  inventory = { items: [], totalCount: 0 } as PagedResultDto<InventoryDto>;

  isModalOpen = false; 
  isIssueModalOpen = false;

  form: FormGroup; 
  issueForm: FormGroup;

  selectedInventory = {} as InventoryDto;
  
  warehouses: WarehouseLookupDto[];
  products: ProductInventoryLookupDto[];

  issueTypes = issueTypeOptions;


  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private inventoryService: WarehouseInventoryService,
    private issueService: IssueService,
    private confirmation: ConfirmationService,
    private productService: ProductInventoryService,
    private warehouseService: WarehouseService,
    ) {

  }

  ngOnInit(): void {
    const inventoryStreamCreator = (query) => this.inventoryService.getList(query);

    this.list.hookToQuery(inventoryStreamCreator).subscribe((response) => {
      this.inventory = response;
    });

    this.productService.getProductInventoryLookup().subscribe(s => {
      this.products = s.items;
    });
    
    this.warehouseService.getWarehouseLookup().subscribe(s => {
      this.warehouses = s.items;
    });
  }

  create() {
    this.selectedInventory= {} as InventoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      warehouseId: [this.selectedInventory.warehouseId || null, Validators.required],
      productId: [this.selectedInventory.productId || null, Validators.required],
      count: [this.selectedInventory.count || null, Validators.required],
      capacity: [this.selectedInventory.capacity || null, Validators.required],
      notes: [this.selectedInventory.notes || '', Validators.maxLength(500)],
    });
  }

  
  edit(id: string) {
    this.inventoryService .get(id).subscribe((product) => {
      this.selectedInventory = product;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedInventory.id) {
      this.inventoryService
        .update(this.selectedInventory.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.inventoryService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.inventoryService.delete(id).subscribe(() => this.list.get());
      }
    });
  } 

  createIssue(id: string) {
    this.isIssueModalOpen = true;
    this.buildIssueForm(id);
  }

  buildIssueForm(id: string) {
    this.issueForm = this.fb.group({
      productInventoryId: [id, null],
      number: ['', Validators.required],
      type: ['', Validators.required],
      entityType: [IssueEntityType.WarehouseInventory, null],
      notes: ['', Validators.required],
      assignee: ['', null],
    });
  }

  saveIssue() {
    if (this.issueForm.invalid) {
      return;
    }

    this.issueService.create(this.issueForm.value).subscribe(() => {
      this.isIssueModalOpen = false;
      this.issueForm.reset();
      this.list.get();
    });
  }

}
