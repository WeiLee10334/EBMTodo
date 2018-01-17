import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectMemberComponent } from './ebm-project-member.component';

describe('EbmProjectMemberComponent', () => {
  let component: EbmProjectMemberComponent;
  let fixture: ComponentFixture<EbmProjectMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
