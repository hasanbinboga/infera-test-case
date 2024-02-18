import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomIssueComponent } from './room-issue.component';

describe('RoomIssueComponent', () => {
  let component: RoomIssueComponent;
  let fixture: ComponentFixture<RoomIssueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoomIssueComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RoomIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
