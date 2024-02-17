import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { productInventoryTypeOptions, issueTypeOptions } from '@proxy';
import { WarehouseService, WarehouseDto } from '@proxy/warehouses';
import { BuildingListFilterDto, BuildingLookupDto, BuildingService, BuildingWarehouseDto } from '@proxy/buildings';
import { BuildingWarehouseService } from '@proxy/building-warehouses';
import { ProductInventoryDto, ProductInventoryService } from '@proxy/product-inventories';
import { IssueService, UserLookupDto } from '@proxy/issues';
 

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent implements OnInit {
  product = { items: [], totalCount: 0 } as PagedResultDto<ProductInventoryDto>;

  isModalOpen = false; 
  isIssueModalOpen = false;

  form: FormGroup; 
  issueForm: FormGroup;

  selectedProduct = {} as ProductInventoryDto;
  
  users: UserLookupDto[];

  productTypes = productInventoryTypeOptions;
  issueTypes = issueTypeOptions;

  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private issueService: IssueService,
    private confirmation: ConfirmationService,
    private productService: ProductInventoryService) {

  }
  ngOnInit(): void {
    const productStreamCreator = (query) => this.productService.getList(query);

    this.list.hookToQuery(productStreamCreator).subscribe((response) => {
      this.product = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

  }

  create() {
    this.selectedProduct = {} as ProductInventoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedProduct.name || '', [Validators.required, Validators.maxLength(255), Validators.minLength(5)]],
      manifacturer: [this.selectedProduct.manifacturer || '', [Validators.required, Validators.maxLength(255)]],
      type: [this.selectedProduct.type || '', Validators.required],
      size: [this.selectedProduct.size || '', Validators.required],
      salePrice: [this.selectedProduct.salePrice || '', Validators.required],
      notes: [this.selectedProduct.notes || '', Validators.maxLength(500)],
    });
  }

  edit(id: string) {
    this.productService.get(id).subscribe((product) => {
      this.selectedProduct = product;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedProduct.id) {
      this.productService
        .update(this.selectedProduct.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.productService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.productService.delete(id).subscribe(() => this.list.get());
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
