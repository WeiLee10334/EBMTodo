import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectDetailComponent } from './ebm-project-detail.component';

describe('EbmProjectDetailComponent', () => {
  let component: EbmProjectDetailComponent;
  let fixture: ComponentFixture<EbmProjectDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
