import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BuildingService, BuildingDto } from '@proxy/buildings';
import { IssueDto } from '@proxy/issues';

@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrl: './building.component.scss'
})
export class BuildingComponent implements OnInit {
  
  building = { items: [], totalCount: 0 } as PagedResultDto<BuildingDto>;
  isModalOpen = false;
  isIssueModalOpen = false;

  form: FormGroup;
  issueForm: FormGroup;
  
  selectedBuilding = {} as BuildingDto;
  
 


  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private buildingService: BuildingService) {
  }

  ngOnInit(): void {
    const buildingStreamCreator = (query) => this.buildingService.getList(query);

    this.list.hookToQuery(buildingStreamCreator).subscribe((response) => {
      this.building = response;
    });
  }

  create(){
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

  edit(id:string){
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
          this.list.get();
        });
    } else {
      this.buildingService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id:string){

  }
  createIssue(id:string){
    this.isIssueModalOpen = true;
    this.buildIssueForm();
  }

  buildIssueForm() {
    this.issueForm = this.fb.group({
      buildingId: [this.selectedBuilding.id, Validators.required],
      number: ['', Validators.required],
      type: ['', Validators.required],
      notes: ['', Validators.required],
      assignee: ['', null],
    });
  }

  saveIssue(){

    this.isIssueModalOpen = false;
    this.issueForm.reset(); 
  }

}
