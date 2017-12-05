import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmtodolistComponent } from './ebmtodolist.component';

describe('EbmtodolistComponent', () => {
  let component: EbmtodolistComponent;
  let fixture: ComponentFixture<EbmtodolistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmtodolistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmtodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
