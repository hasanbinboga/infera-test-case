import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { productInventoryTypeOptions, issueTypeOptions, IssueEntityType, accountingTypeOptions } from '@proxy';
import { ProductInventoryDto, ProductInventoryLookupDto, ProductInventoryService } from '@proxy/product-inventories';
import { IssueService, UserLookupDto } from '@proxy/issues';
import { AccountingDto, AccountingService } from '@proxy/accountings';

@Component({
  selector: 'app-accounting',
  templateUrl: './accounting.component.html',
  styleUrl: './accounting.component.scss'
})
export class AccountingComponent implements OnInit {

  accounting = { items: [], totalCount: 0 } as PagedResultDto<AccountingDto>;

  isModalOpen = false; 

  form: FormGroup; 

  selectedAccounting = {} as AccountingDto;

  products: ProductInventoryLookupDto[];

  accountingTypes = accountingTypeOptions;

  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private accountingService: AccountingService,
    private productService: ProductInventoryService) {

  }

  ngOnInit(): void {
    const accountingStreamCreator = (query) => this.accountingService.getList(query);

    this.list.hookToQuery(accountingStreamCreator).subscribe((response) => {
      this.accounting = response;
    });

    this.productService.getProductInventoryLookup().subscribe(s => {
      this.products = s.items;
    });

  }

  create() {
    this.selectedAccounting = {} as AccountingDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      productInventoryId: [this.selectedAccounting.productInventoryId || null, Validators.required],
      saleOrderId: [this.selectedAccounting.saleOrderId || null, null],
      type: [this.selectedAccounting.type || null, Validators.required],
      count: [this.selectedAccounting.count || null, Validators.required],
      purchasePrice: [this.selectedAccounting.purchasePrice || null, Validators.required],
      salePrice: [this.selectedAccounting.salePrice || null, Validators.required],
      amount: [this.selectedAccounting.amount || null, Validators.required],
      tax: [this.selectedAccounting.tax || null, Validators.required],
      invoiceDate: [this.selectedAccounting.invoiceDate || null, null],
      invoiceNumber: [this.selectedAccounting.invoiceNumber || null, null]
    });
  }

  edit(id: string) {
    this.accountingService.get(id).subscribe((product) => {
      this.selectedAccounting = product;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedAccounting.id) {
      this.accountingService
        .update(this.selectedAccounting.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.accountingService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.accountingService.delete(id).subscribe(() => this.list.get());
      }
    });
  } 


}
