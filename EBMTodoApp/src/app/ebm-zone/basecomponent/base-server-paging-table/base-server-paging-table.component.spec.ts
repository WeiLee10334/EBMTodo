import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseServerPagingTableComponent } from './base-server-paging-table.component';

describe('BaseServerPagingTableComponent', () => {
  let component: BaseServerPagingTableComponent;
  let fixture: ComponentFixture<BaseServerPagingTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseServerPagingTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseServerPagingTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
