import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWorkingRowComponent } from './project-working-row.component';

describe('ProjectWorkingRowComponent', () => {
  let component: ProjectWorkingRowComponent;
  let fixture: ComponentFixture<ProjectWorkingRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectWorkingRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectWorkingRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
