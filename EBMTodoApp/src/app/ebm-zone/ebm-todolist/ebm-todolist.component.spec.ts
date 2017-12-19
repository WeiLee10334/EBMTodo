import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmTodolistComponent } from './ebm-todolist.component';

describe('EbmTodolistComponent', () => {
  let component: EbmTodolistComponent;
  let fixture: ComponentFixture<EbmTodolistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmTodolistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmTodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
