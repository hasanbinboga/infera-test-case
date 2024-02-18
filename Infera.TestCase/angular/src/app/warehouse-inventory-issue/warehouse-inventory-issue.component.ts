import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueEntityTypeOptions, issueTypeOptions } from '@proxy';
import { BuildingLookupDto, BuildingService } from '@proxy/buildings';
import { IssueDto, IssueListFilterDto, IssueService, UserLookupDto } from '@proxy/issues';

@Component({
  selector: 'app-warehouse-inventory-issue',
  templateUrl: './warehouse-inventory-issue.component.html',
  styleUrl: './warehouse-inventory-issue.component.scss'
})
export class WarehouseInventoryIssueComponent {
issue = { items: [], totalCount: 0 } as PagedResultDto<IssueDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedIssue = {} as IssueDto;

  issueTypes = issueTypeOptions;
  issueEntityTypes = issueEntityTypeOptions;

  users: UserLookupDto[];
  buildings: BuildingLookupDto[];
}
