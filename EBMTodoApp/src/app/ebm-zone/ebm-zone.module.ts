import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EbmZoneRoutingModule } from './ebm-zone-routing.module';
import { EbmZoneComponent } from './ebm-zone.component';
import { EbmWorkingComponent } from './ebm-working/ebm-working.component';
import { SharedModule } from '../shared/modules/index';
import { AccordionModule, ProgressbarModule, BsDropdownModule, PaginationModule, CollapseModule } from 'ngx-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { EbmScheduleComponent } from './ebm-schedule/ebm-schedule.component';
import { EbmMemoComponent } from './ebm-memo/ebm-memo.component';
import { EbmOnlineComponent } from './ebm-online/ebm-online.component';
import { EbmProjectComponent } from './ebm-project/ebm-project.component';
import { ProjectTableRowComponent } from './components/project-table-row/project-table-row.component';
import { EbmProjectMemberComponent } from './ebm-project/ebm-project-member/ebm-project-member.component';
import { ProjectMemberTableRowComponent } from './components/project-member-table-row/project-member-table-row.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { EbmUserComponent } from './ebm-user/ebm-user.component';
import { EbmProjectWorkingComponent } from './ebm-project/ebm-project-working/ebm-project-working.component';
import { UserTableRowComponent } from './components/user-table-row/user-table-row.component';
import { EbmProjectTodolistComponent } from './ebm-project/ebm-project-todolist/ebm-project-todolist.component';
import { ProjectWorkingTableRowComponent } from './components/project-working-table-row/project-working-table-row.component';
import { CustomSelectInputComponent } from './components/custom-select-input/custom-select-input.component';
import { ProjectTodolistCardComponent } from './components/project-todolist-card/project-todolist-card.component';
import { EbmProjectListComponent } from './ebm-project/ebm-project-list/ebm-project-list.component';
import { EbmUserListComponent } from './ebm-user/ebm-user-list/ebm-user-list.component';
import { EbmUserMemberComponent } from './ebm-user/ebm-user-member/ebm-user-member.component';
import { EbmUserTodolistComponent } from './ebm-user/ebm-user-todolist/ebm-user-todolist.component';
import { BaseServerPagingTableComponent } from './basecomponent/base-server-paging-table/base-server-paging-table.component';
import { EbmUserWorkingComponent } from './ebm-user/ebm-user-working/ebm-user-working.component';
import { EbmOnlineTableRowComponent } from './components/ebm-online-table-row/ebm-online-table-row.component';
import { BaseTableRowComponent } from './basecomponent/base-table-row/base-table-row.component';
import { ContentEditableDirective } from '../shared/directives/content-editable.directive';
import { MenuComponent } from '../shared/components/menu/menu.component';
import { EbmProjectDetailComponent } from './ebm-project/ebm-project-detail/ebm-project-detail.component';
import { JDatetimepickerDirective } from '../shared/directives/j-datetimepicker.directive';
import { EbmProjectScheduleComponent } from './ebm-project/ebm-project-schedule/ebm-project-schedule.component';

@NgModule({
  imports: [
    CommonModule,
    EbmZoneRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    ProgressbarModule.forRoot(),
    SharedModule,
    InfiniteScrollModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    CollapseModule.forRoot()
  ],
  declarations: [
    EbmZoneComponent,
    EbmWorkingComponent,
    EbmScheduleComponent,
    EbmMemoComponent,
    EbmOnlineComponent,
    EbmProjectComponent,
    ProjectTableRowComponent,
    EbmProjectMemberComponent,
    ProjectMemberTableRowComponent,
    EbmUserComponent,
    EbmProjectWorkingComponent,
    UserTableRowComponent,
    EbmProjectTodolistComponent,
    ProjectWorkingTableRowComponent,
    CustomSelectInputComponent,
    ProjectTodolistCardComponent,
    EbmProjectListComponent,
    EbmUserListComponent,
    EbmUserMemberComponent,
    EbmUserTodolistComponent,
    BaseServerPagingTableComponent,
    EbmUserWorkingComponent,
    EbmOnlineTableRowComponent,
    BaseTableRowComponent,
    ContentEditableDirective,
    MenuComponent,
    EbmProjectDetailComponent,
    JDatetimepickerDirective,
    EbmProjectScheduleComponent
  ]
})
export class EbmZoneModule { }
