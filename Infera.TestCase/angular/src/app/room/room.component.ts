import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { issueTypeOptions } from '@proxy';
import { RoomService, RoomDto } from '@proxy/rooms';
import { IssueService, UserLookupDto } from '@proxy/issues';


@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrl: './room.component.scss'
})
export class RoomComponent implements OnInit {

  room = { items: [], totalCount: 0 } as PagedResultDto<RoomDto>;
  isModalOpen = false;
  isIssueModalOpen = false;

  form: FormGroup;
  issueForm: FormGroup;

  selectedRoom = {} as RoomDto;

  users: UserLookupDto[];

  issueTypes = issueTypeOptions;
  constructor(public readonly list: ListService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private roomService: RoomService,
    private issueService: IssueService,
  ) {

  }

  ngOnInit(): void {
    const roomStreamCreator = (query) => this.roomService.getList(query);

    this.list.hookToQuery(roomStreamCreator).subscribe((response) => {
      this.room = response;
    });

    this.issueService.getUserLookup().subscribe(s => {
      this.users = s.items;
    });

  }

  create() {
    this.selectedRoom = {} as RoomDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({

      buildingId: [this.selectedRoom.buildingId || '', [Validators.required]],
      no: [this.selectedRoom.no || '', [Validators.required, Validators.maxLength(20)]],
      floor: [this.selectedRoom.floor || '', Validators.required],
      capacity: [this.selectedRoom.capacity || '', null],
      hasMiniBar: [this.selectedRoom.hasMiniBar || '', null],
      notes: [this.selectedRoom.notes || '', Validators.maxLength(500)]
    });
  }

  edit(id: string) {
    this.roomService.get(id).subscribe((room) => {
      this.selectedRoom = room;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedRoom.id) {
      this.roomService
        .update(this.selectedRoom.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.roomService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.roomService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  createIssue(id: string) {
    this.isIssueModalOpen = true;
    this.buildIssueForm();
  }

  buildIssueForm() {
    this.issueForm = this.fb.group({
      roomId: [this.selectedRoom.id, null],
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
    });
  }
}
