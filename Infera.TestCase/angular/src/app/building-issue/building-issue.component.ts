import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueEntityTypeOptions, issueTypeOptions } from '@proxy';
import { BuildingWarehouseService } from '@proxy/building-warehouses'; 
import { BuildingLookupDto, BuildingService } from '@proxy/buildings';
import { IssueDto, IssueListFilterDto, IssueService, UserLookupDto } from '@proxy/issues';
import { WarehouseListFilterDto, WarehouseLookupDto, WarehouseService } from '@proxy/warehouses';

@Component({
  selector: 'app-building-issue',
  templateUrl: './building-issue.component.html',
  styleUrl: './building-issue.component.scss'
})
export class BuildingIssueComponent implements OnInit {

  issue = { items: [], totalCount: 0 } as PagedResultDto<IssueDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedIssue= {} as IssueDto;

  issueTypes = issueTypeOptions;
  issueEntityTypes = issueEntityTypeOptions;

  users: UserLookupDto[];
  buildings: BuildingLookupDto[];








  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private issueService: IssueService,
    private buildingService: BuildingService,
    private confirmation: ConfirmationService) {
    
  }


  ngOnInit(): void {
    const issueStreamCreator = (query:IssueListFilterDto) => {
      query.entityType = IssueEntityType.Building;
      return this.issueService.getListByEntityType(query);
    };

    this.list.hookToQuery(issueStreamCreator).subscribe((response) => {
      this.issue = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

    this.buildingService.getBuildingLookup().subscribe(s => {
      this.buildings = s.items;
    });
  }

  create() {
    this.selectedIssue = {} as IssueDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      buildingId: [this.selectedIssue.buildingId || null, Validators.required],
      assignee: [this.selectedIssue.assigneeId || null, null],
      number: [this.selectedIssue.number || null, null],
      type: [this.selectedIssue.type || null, Validators.required],
      entityType: [this.selectedIssue.entityType || IssueEntityType.Building,  Validators.required],
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

    console.log("this.form.value",this.form.value);
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
