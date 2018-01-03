import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EbmProjectMemberListComponent } from './ebm-project-member-list.component';

describe('EbmProjectMemberListComponent', () => {
  let component: EbmProjectMemberListComponent;
  let fixture: ComponentFixture<EbmProjectMemberListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EbmProjectMemberListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EbmProjectMemberListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
