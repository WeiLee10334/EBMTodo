import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectScheduleComponent } from './ebm-project-schedule.component';

describe('EbmProjectScheduleComponent', () => {
  let component: EbmProjectScheduleComponent;
  let fixture: ComponentFixture<EbmProjectScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
