import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmUserListComponent } from './ebm-user-list.component';

describe('EbmUserListComponent', () => {
  let component: EbmUserListComponent;
  let fixture: ComponentFixture<EbmUserListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmUserListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
