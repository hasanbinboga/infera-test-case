import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueEntityTypeOptions, issueTypeOptions } from '@proxy';
import { IssueDto, IssueListFilterDto, IssueService, UserLookupDto } from '@proxy/issues';
import { InventoryLookupDto, WarehouseInventoryService } from '@proxy/warehouse-inventories';

@Component({
  selector: 'app-warehouse-inventory-issue',
  templateUrl: './warehouse-inventory-issue.component.html',
  styleUrl: './warehouse-inventory-issue.component.scss'
})
export class WarehouseInventoryIssueComponent implements OnInit {
  issue = { items: [], totalCount: 0 } as PagedResultDto<IssueDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedIssue = {} as IssueDto;

  issueTypes = issueTypeOptions;
  issueEntityTypes = issueEntityTypeOptions;

  users: UserLookupDto[];
  inventories: InventoryLookupDto[];

  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private issueService: IssueService,
    private inventoryService: WarehouseInventoryService,
    private confirmation: ConfirmationService) {
    
  }

  ngOnInit(): void {
    const issueStreamCreator = (query:IssueListFilterDto) => {
      query.entityType = IssueEntityType.WarehouseInventory;
      return this.issueService.getListByEntityType(query);
    };

    this.list.hookToQuery(issueStreamCreator).subscribe((response) => {
      this.issue = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

    this.inventoryService.getInventoryLookup().subscribe(s => {
      this.inventories = s.items;
    });
    
  }

  create() {
    this.selectedIssue = {} as IssueDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      warehouseInventoryId: [this.selectedIssue.warehouseInventoryId || null, Validators.required],
      assignee: [this.selectedIssue.assigneeId || null, null],
      number: [this.selectedIssue.number || null, null],
      type: [this.selectedIssue.type || null, Validators.required],
      entityType: [this.selectedIssue.entityType || IssueEntityType.WarehouseInventory,  Validators.required],
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
