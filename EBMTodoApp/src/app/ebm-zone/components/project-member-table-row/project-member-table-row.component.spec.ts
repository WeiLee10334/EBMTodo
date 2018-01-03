import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMemberTableRowComponent } from './project-member-table-row.component';

describe('ProjectMemberTableRowComponent', () => {
  let component: ProjectMemberTableRowComponent;
  let fixture: ComponentFixture<ProjectMemberTableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectMemberTableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectMemberTableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
