import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineTableRowComponent } from './online-table-row.component';

describe('OnlineTableRowComponent', () => {
  let component: OnlineTableRowComponent;
  let fixture: ComponentFixture<OnlineTableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlineTableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineTableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
