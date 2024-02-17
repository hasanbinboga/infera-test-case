import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { issueTypeOptions } from '@proxy';
import { WarehouseService, WarehouseDto } from '@proxy/warehouses';
import { IssueService, UserLookupDto } from '@proxy/issues';
import { BuildingLookupDto, BuildingService } from '@proxy/buildings';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrl: './warehouse.component.scss'
})
export class WarehouseComponent implements OnInit {

  warehouse = { items: [], totalCount: 0 } as PagedResultDto<WarehouseDto>;
  isModalOpen = false; 

  form: FormGroup; 
  buildingWarehouseForm: FormGroup;

  selectedWarehouse = {} as WarehouseDto;

  users: UserLookupDto[];
  buildings: BuildingLookupDto[];

  issueTypes = issueTypeOptions;


  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private warehouseService: WarehouseService,
    private buildingService: BuildingService,
    private issueService: IssueService,
  ) {

  }

  ngOnInit(): void {
    const warehouseStreamCreator = (query) => this.warehouseService.getList(query);

    this.list.hookToQuery(warehouseStreamCreator).subscribe((response) => {
      this.warehouse = response;
    });

    this.issueService.getUserLookup().subscribe(s=>{
      this.users = s.items;
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
          this.list.get();
        });
    } else {
      this.warehouseService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.warehouseService.delete(id).subscribe(() => this.list.get());
      }
    });
  } 

  createBuildingWarehouse(id: string){
    
  }

  buildBuildingWarehouseForm() {
    
  }

  saveBuildingWarehouse(){
    
  }
}
