import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmUserTodolistComponent } from './ebm-user-todolist.component';

describe('EbmUserTodolistComponent', () => {
  let component: EbmUserTodolistComponent;
  let fixture: ComponentFixture<EbmUserTodolistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmUserTodolistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmUserTodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
