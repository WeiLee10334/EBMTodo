import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineTableComponent } from './online-table.component';

describe('OnlineTableComponent', () => {
  let component: OnlineTableComponent;
  let fixture: ComponentFixture<OnlineTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlineTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
