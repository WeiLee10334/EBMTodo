import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupbytimeComponent } from './groupbytime.component';

describe('GroupbytimeComponent', () => {
  let component: GroupbytimeComponent;
  let fixture: ComponentFixture<GroupbytimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupbytimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupbytimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
