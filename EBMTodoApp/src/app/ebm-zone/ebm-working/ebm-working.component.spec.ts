import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmWorkingComponent } from './ebm-working.component';

describe('EbmWorkingComponent', () => {
  let component: EbmWorkingComponent;
  let fixture: ComponentFixture<EbmWorkingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmWorkingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmWorkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
