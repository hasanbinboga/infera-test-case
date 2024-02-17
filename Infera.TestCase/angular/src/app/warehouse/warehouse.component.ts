import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { issueTypeOptions } from '@proxy';
import { WarehouseService, WarehouseDto } from '@proxy/warehouses';
import { BuildingListFilterDto, BuildingLookupDto, BuildingService, BuildingWarehouseDto } from '@proxy/buildings';
import { BuildingWarehouseService } from '@proxy/building-warehouses';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrl: './warehouse.component.scss'
})
export class WarehouseComponent implements OnInit {

  warehouse = { items: [], totalCount: 0 } as PagedResultDto<WarehouseDto>;
  building = { items: [], totalCount: 0 } as PagedResultDto<BuildingWarehouseDto>;
  
  isModalOpen = false; 
  isBuildingListModalOpen = false;
  isLinkToBuildingModalOpen = false;

  form: FormGroup; 
  buildingWarehouseForm: FormGroup;

  selectedWarehouse = {} as WarehouseDto;
  selectedBuildingWarehouse = {} as BuildingWarehouseDto;

  buildings: BuildingLookupDto[];

  issueTypes = issueTypeOptions;


  constructor(public readonly buildingList: ListService,
    public readonly warehouseList: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private warehouseService: WarehouseService,
    private buildingWarehouseService: BuildingWarehouseService,
    private buildingService: BuildingService,
  ) {

  }

  ngOnInit(): void {
    const warehouseStreamCreator = (query) => this.warehouseService.getList(query);

    this.warehouseList.hookToQuery(warehouseStreamCreator).subscribe((response) => {
      this.warehouse = response;
    });


    this.buildingService.getBuildingLookup().subscribe(s=>{
      this.buildings = s.items;
    });
    
  }


  create() {
    this.selectedWarehouse = {} as WarehouseDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      buildingId: [this.selectedWarehouse.buildingId || '', null],
      name: [this.selectedWarehouse.name || '', [Validators.required, Validators.maxLength(255), Validators.minLength(2)]],
      no: [this.selectedWarehouse.no || '', [Validators.required, Validators.maxLength(20)]],
      floor: [this.selectedWarehouse.floor || '', Validators.required],
      capacity: [this.selectedWarehouse.capacity || '', null],
      content: [this.selectedWarehouse.content || '', Validators.maxLength(500)],
      notes: [this.selectedWarehouse.notes || '', Validators.maxLength(500)]
    });
  }

  edit(id: string) {
    this.warehouseService.get(id).subscribe((warehouse) => {
      this.selectedWarehouse = warehouse;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedWarehouse.id) {
      this.warehouseService
        .update(this.selectedWarehouse.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.warehouseList.get();
        });
    } else {
      this.warehouseService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.warehouseList.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.warehouseService.delete(id).subscribe(() => this.warehouseList.get());
      }
    });
  } 

  listBuildings(id: string) {

    const buildingStreamCreator = (input: BuildingListFilterDto) => {
      input.warehouseId = id;
      return this.buildingService.getListOfWarehouse(input);
    };

    this.buildingList.hookToQuery(buildingStreamCreator).subscribe((response) => {
      this.building = response;
      this.isBuildingListModalOpen = true;
    });

  }

  linkToBuilding(id: string) {
    this.isLinkToBuildingModalOpen = true;
    this.buildBuildingWarehouseForm(id);
  }

  buildBuildingWarehouseForm(id: string) {
    this.buildingWarehouseForm = this.fb.group({
      warehouseId: [id, null],
      buildingId: ['', Validators.required]
    });
  }

  saveBuildingWarehouse(){
    if (this.buildingWarehouseForm.invalid) {
      return;
    }
    this.buildingWarehouseService.create(this.buildingWarehouseForm.value).subscribe(() => {
      this.isLinkToBuildingModalOpen = false;
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
