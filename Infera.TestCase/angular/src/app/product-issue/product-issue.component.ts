import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueEntityTypeOptions, issueTypeOptions } from '@proxy';
import { BuildingLookupDto, BuildingService } from '@proxy/buildings';
import { IssueDto, IssueListFilterDto, IssueService, UserLookupDto } from '@proxy/issues';
import { ProductInventoryLookupDto, ProductInventoryService } from '@proxy/product-inventories';

@Component({
  selector: 'app-product-issue',
  templateUrl: './product-issue.component.html',
  styleUrl: './product-issue.component.scss'
})
export class ProductIssueComponent implements OnInit {

  issue = { items: [], totalCount: 0 } as PagedResultDto<IssueDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedIssue= {} as IssueDto;

  issueTypes = issueTypeOptions;
  issueEntityTypes = issueEntityTypeOptions;

  users: UserLookupDto[];
  products: ProductInventoryLookupDto[];

  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private issueService: IssueService,
    private productService: ProductInventoryService,
    private confirmation: ConfirmationService) {
    
  }

  
  ngOnInit(): void {
    const issueStreamCreator = (query:IssueListFilterDto) => {
      query.entityType = IssueEntityType.ProductInventory;
      return this.issueService.getListByEntityType(query);
    };

    this.list.hookToQuery(issueStreamCreator).subscribe((response) => {
      this.issue = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

    this.productService.getProductInventoryLookup().subscribe(s => {
      this.products = s.items;
    });
    
  }

  create() {
    this.selectedIssue = {} as IssueDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      productInventoryId: [this.selectedIssue.productInventoryId || null, Validators.required],
      assignee: [this.selectedIssue.assigneeId || null, null],
      number: [this.selectedIssue.number || null, null],
      type: [this.selectedIssue.type || null, Validators.required],
      entityType: [this.selectedIssue.entityType || IssueEntityType.ProductInventory,  Validators.required],
      notes: [this.selectedIssue.notes || "", Validators.maxLength(500)],
      isCompleted: [this.selectedIssue.isCompleted || null, null],
      completedTime: [this.selectedIssue.completedTime ? new Date(this.selectedIssue.completedTime) : null, null],
    });
    
  }


  edit(id: string) {
    this.issueService.get(id).subscribe((issue) => {
      this.selectedIssue = issue;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedIssue.id) {
      this.issueService
        .update(this.selectedIssue.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.issueService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.issueService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

}
