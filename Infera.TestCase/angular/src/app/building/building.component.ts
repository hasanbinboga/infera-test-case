import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IssueEntityType, issueTypeOptions } from '@proxy';
import { BuildingWarehouseService } from '@proxy/building-warehouses';
import { BuildingService, BuildingDto, BuildingWarehouseDto} from '@proxy/buildings';
import { IssueService, UserLookupDto } from '@proxy/issues';
import { WarehouseListFilterDto, WarehouseLookupDto, WarehouseService } from '@proxy/warehouses';

@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrl: './building.component.scss'
})
export class BuildingComponent implements OnInit {

  building = { items: [], totalCount: 0 } as PagedResultDto<BuildingDto>;
  warehouse = { items: [], totalCount: 0 } as PagedResultDto<BuildingWarehouseDto>;

  isModalOpen = false;
  isIssueModalOpen = false;
  isWharehouseListModalOpen = false;
  isLinkToWarehouseModalOpen = false;

  form: FormGroup;
  buildingWarehouseForm: FormGroup;
  issueForm: FormGroup;

  selectedBuilding = {} as BuildingDto;
  selectedBuildingWarehouse = {} as BuildingWarehouseDto;

  users: UserLookupDto[];
  warehouses: WarehouseLookupDto[];

  issueTypes = issueTypeOptions;


  constructor(public readonly buildingList: ListService,
    public readonly warehouseList: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private buildingService: BuildingService,
    private warehouseService: WarehouseService,
    private buildingWarehouseService: BuildingWarehouseService,
    private issueService: IssueService,
  ) {

  }

  ngOnInit(): void {
    const buildingStreamCreator = (query) => this.buildingService.getList(query);

    this.buildingList.hookToQuery(buildingStreamCreator).subscribe((response) => {
      this.building = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

    this.warehouseService.getWarehouseLookup().subscribe(s => {
      this.warehouses = s.items;
    });

  }

  create() {
    this.selectedBuilding = {} as BuildingDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedBuilding.name || '', [Validators.required, Validators.maxLength(255), Validators.minLength(2)]],
      no: [this.selectedBuilding.no || '', [Validators.required, Validators.maxLength(20)]],
      addres: [this.selectedBuilding.addres || '', Validators.maxLength(500)]
    });
  }

  edit(id: string) {
    this.buildingService.get(id).subscribe((building) => {
      this.selectedBuilding = building;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedBuilding.id) {
      this.buildingService
        .update(this.selectedBuilding.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.buildingList.get();
        });
    } else {
      this.buildingService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.buildingList.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.buildingService.delete(id).subscribe(() => this.buildingList.get());
      }
    });
  }

  createIssue(id: string) {
    this.isIssueModalOpen = true;
    this.buildIssueForm();
  }

  buildIssueForm() {
    this.issueForm = this.fb.group({
      buildingId: [this.selectedBuilding.id, null],
      number: ['', Validators.required],
      type: ['', Validators.required],
      entityType: [IssueEntityType.Building, null],
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
    });

  }

  listWarehouses(id: string) {

    const warehouseStreamCreator = (input: WarehouseListFilterDto) => {
      input.buildingId = id;
      return this.warehouseService.getListOfBuilding(input);
    };

    this.warehouseList.hookToQuery(warehouseStreamCreator).subscribe((response) => {
      this.warehouse = response;
      this.isWharehouseListModalOpen = true;
    });

  }


  linkToWarehouse(id: string) {
    this.isLinkToWarehouseModalOpen = true;
    this.buildBuildingWarehouseForm(id);
  }

  buildBuildingWarehouseForm(id: string) {
    this.buildingWarehouseForm = this.fb.group({
      buildingId: [id, null],
      warehouseId: ['', Validators.required]
    });
  }

  saveBuildingWarehouse() {
    if (this.buildingWarehouseForm.invalid) {
      return;
    }
    this.buildingWarehouseService.create(this.buildingWarehouseForm.value).subscribe(() => {
      this.isLinkToWarehouseModalOpen = false;
      this.buildingWarehouseForm.reset();
      this.buildingList.get();
    });
  }

  deleteWarehouseRelation(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.buildingWarehouseService.delete(id).subscribe(() => {
          this.warehouseList.get();
          this.buildingList.get();

        }
        );
      }
    });
  }

}
