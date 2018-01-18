import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmOnlineTableRowComponent } from './ebm-online-table-row.component';

describe('EbmOnlineTableRowComponent', () => {
  let component: EbmOnlineTableRowComponent;
  let fixture: ComponentFixture<EbmOnlineTableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmOnlineTableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmOnlineTableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
