import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmtodolistListComponent } from './ebmtodolist-list.component';

describe('EbmtodolistListComponent', () => {
  let component: EbmtodolistListComponent;
  let fixture: ComponentFixture<EbmtodolistListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmtodolistListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmtodolistListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
