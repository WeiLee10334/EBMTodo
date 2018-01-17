import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmUserMemberComponent } from './ebm-user-member.component';

describe('EbmUserMemberComponent', () => {
  let component: EbmUserMemberComponent;
  let fixture: ComponentFixture<EbmUserMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmUserMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmUserMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
