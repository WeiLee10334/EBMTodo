import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmscheduleListComponent } from './ebmschedule-list.component';

describe('EbmscheduleListComponent', () => {
  let component: EbmscheduleListComponent;
  let fixture: ComponentFixture<EbmscheduleListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmscheduleListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmscheduleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
