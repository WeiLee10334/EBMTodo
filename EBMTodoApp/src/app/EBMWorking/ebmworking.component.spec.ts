import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmworkingComponent } from './ebmworking.component';

describe('EbmworkingComponent', () => {
  let component: EbmworkingComponent;
  let fixture: ComponentFixture<EbmworkingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmworkingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmworkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
