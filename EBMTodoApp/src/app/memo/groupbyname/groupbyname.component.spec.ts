import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupbynameComponent } from './groupbyname.component';

describe('GroupbynameComponent', () => {
  let component: GroupbynameComponent;
  let fixture: ComponentFixture<GroupbynameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupbynameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupbynameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
