import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWorkingTableRowComponent } from './project-working-table-row.component';

describe('ProjectWorkingTableRowComponent', () => {
  let component: ProjectWorkingTableRowComponent;
  let fixture: ComponentFixture<ProjectWorkingTableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectWorkingTableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectWorkingTableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
