import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmOnlineComponent } from './ebm-online.component';

describe('EbmOnlineComponent', () => {
  let component: EbmOnlineComponent;
  let fixture: ComponentFixture<EbmOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
