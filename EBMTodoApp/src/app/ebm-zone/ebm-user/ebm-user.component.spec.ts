import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmUserComponent } from './ebm-user.component';

describe('EbmUserComponent', () => {
  let component: EbmUserComponent;
  let fixture: ComponentFixture<EbmUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
