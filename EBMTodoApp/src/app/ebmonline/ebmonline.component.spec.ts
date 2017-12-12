import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmonlineComponent } from './ebmonline.component';

describe('EbmonlineComponent', () => {
  let component: EbmonlineComponent;
  let fixture: ComponentFixture<EbmonlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmonlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmonlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
