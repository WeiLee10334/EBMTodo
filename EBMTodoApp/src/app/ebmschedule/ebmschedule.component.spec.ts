import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmscheduleComponent } from './ebmschedule.component';

describe('EbmscheduleComponent', () => {
  let component: EbmscheduleComponent;
  let fixture: ComponentFixture<EbmscheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmscheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmscheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
