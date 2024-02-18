import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingIssueComponent } from './building-issue.component';

describe('BuildingIssueComponent', () => {
  let component: BuildingIssueComponent;
  let fixture: ComponentFixture<BuildingIssueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BuildingIssueComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuildingIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
