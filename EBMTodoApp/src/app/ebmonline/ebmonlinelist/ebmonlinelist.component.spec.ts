import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmonlinelistComponent } from './ebmonlinelist.component';

describe('EbmonlinelistComponent', () => {
  let component: EbmonlinelistComponent;
  let fixture: ComponentFixture<EbmonlinelistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmonlinelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmonlinelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
