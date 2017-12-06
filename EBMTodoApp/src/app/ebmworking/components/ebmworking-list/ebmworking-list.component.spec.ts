import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmworkingListComponent } from './ebmworking-list.component';

describe('EbmworkingListComponent', () => {
  let component: EbmworkingListComponent;
  let fixture: ComponentFixture<EbmworkingListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmworkingListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmworkingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
