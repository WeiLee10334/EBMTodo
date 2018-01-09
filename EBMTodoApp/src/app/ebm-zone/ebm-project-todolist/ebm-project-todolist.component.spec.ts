import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectTodolistComponent } from './ebm-project-todolist.component';

describe('EbmProjectTodolistComponent', () => {
  let component: EbmProjectTodolistComponent;
  let fixture: ComponentFixture<EbmProjectTodolistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectTodolistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectTodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
