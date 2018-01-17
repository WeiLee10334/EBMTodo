import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTodolistCardComponent } from './project-todolist-card.component';

describe('ProjectTodolistCardComponent', () => {
  let component: ProjectTodolistCardComponent;
  let fixture: ComponentFixture<ProjectTodolistCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectTodolistCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTodolistCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
