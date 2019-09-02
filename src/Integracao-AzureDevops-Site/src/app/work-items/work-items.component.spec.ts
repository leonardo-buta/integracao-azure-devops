import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkItemsComponent } from './work-items.component';

describe('WorkItemsComponent', () => {
  let component: WorkItemsComponent;
  let fixture: ComponentFixture<WorkItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
