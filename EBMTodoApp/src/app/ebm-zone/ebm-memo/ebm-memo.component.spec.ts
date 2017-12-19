import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmMemoComponent } from './ebm-memo.component';

describe('EbmMemoComponent', () => {
  let component: EbmMemoComponent;
  let fixture: ComponentFixture<EbmMemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmMemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmMemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
