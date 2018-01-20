import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePagingTableComponent } from './base-paging-table.component';

describe('BasePagingTableComponent', () => {
  let component: BasePagingTableComponent;
  let fixture: ComponentFixture<BasePagingTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BasePagingTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BasePagingTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
