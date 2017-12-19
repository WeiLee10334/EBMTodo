import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmScheduleComponent } from './ebm-schedule.component';

describe('EbmScheduleComponent', () => {
  let component: EbmScheduleComponent;
  let fixture: ComponentFixture<EbmScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
