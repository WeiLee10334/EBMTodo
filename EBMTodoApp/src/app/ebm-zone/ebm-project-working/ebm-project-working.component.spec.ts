import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectWorkingComponent } from './ebm-project-working.component';

describe('EbmProjectWorkingComponent', () => {
  let component: EbmProjectWorkingComponent;
  let fixture: ComponentFixture<EbmProjectWorkingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectWorkingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectWorkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
