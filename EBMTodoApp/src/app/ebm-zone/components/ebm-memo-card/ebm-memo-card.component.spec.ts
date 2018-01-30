import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmMemoCardComponent } from './ebm-memo-card.component';

describe('EbmMemoCardComponent', () => {
  let component: EbmMemoCardComponent;
  let fixture: ComponentFixture<EbmMemoCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmMemoCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmMemoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
