import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectComponent } from './ebm-project.component';

describe('EbmProjectComponent', () => {
  let component: EbmProjectComponent;
  let fixture: ComponentFixture<EbmProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
