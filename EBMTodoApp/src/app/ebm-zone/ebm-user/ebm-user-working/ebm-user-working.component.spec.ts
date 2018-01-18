import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmUserWorkingComponent } from './ebm-user-working.component';

describe('EbmUserWorkingComponent', () => {
  let component: EbmUserWorkingComponent;
  let fixture: ComponentFixture<EbmUserWorkingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmUserWorkingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmUserWorkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
