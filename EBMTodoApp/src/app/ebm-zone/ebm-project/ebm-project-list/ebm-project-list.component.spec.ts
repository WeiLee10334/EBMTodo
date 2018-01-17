import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectListComponent } from './ebm-project-list.component';

describe('EbmProjectListComponent', () => {
  let component: EbmProjectListComponent;
  let fixture: ComponentFixture<EbmProjectListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
