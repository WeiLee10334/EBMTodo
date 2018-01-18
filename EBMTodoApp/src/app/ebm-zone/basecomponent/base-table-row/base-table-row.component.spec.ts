import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseTableRowComponent } from './base-table-row.component';

describe('BaseTableRowComponent', () => {
  let component: BaseTableRowComponent;
  let fixture: ComponentFixture<BaseTableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseTableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseTableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
