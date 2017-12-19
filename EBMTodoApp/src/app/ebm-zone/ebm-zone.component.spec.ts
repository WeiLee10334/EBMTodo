import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmZoneComponent } from './ebm-zone.component';

describe('EbmZoneComponent', () => {
  let component: EbmZoneComponent;
  let fixture: ComponentFixture<EbmZoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmZoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmZoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
