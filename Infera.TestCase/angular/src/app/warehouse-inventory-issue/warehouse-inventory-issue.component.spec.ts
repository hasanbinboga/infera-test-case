import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseInventoryIssueComponent } from './warehouse-inventory-issue.component';

describe('WarehouseInventoryIssueComponent', () => {
  let component: WarehouseInventoryIssueComponent;
  let fixture: ComponentFixture<WarehouseInventoryIssueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WarehouseInventoryIssueComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WarehouseInventoryIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
