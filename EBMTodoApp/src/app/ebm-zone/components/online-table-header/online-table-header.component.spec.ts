import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineTableHeaderComponent } from './online-table-header.component';

describe('OnlineTableHeaderComponent', () => {
  let component: OnlineTableHeaderComponent;
  let fixture: ComponentFixture<OnlineTableHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlineTableHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineTableHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
