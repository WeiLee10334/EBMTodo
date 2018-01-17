import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EbmZoneRoutingModule } from './ebm-zone-routing.module';
import { EbmZoneComponent } from './ebm-zone.component';
import { EbmWorkingComponent } from './ebm-working/ebm-working.component';
import { SharedModule } from '../shared/modules/index';
import { AccordionModule, BsDatepickerModule, ProgressbarModule, BsDropdownModule, PaginationModule } from 'ngx-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { EbmScheduleComponent } from './ebm-schedule/ebm-schedule.component';
import { EbmMemoComponent } from './ebm-memo/ebm-memo.component';
import { EbmTodolistComponent } from './ebm-todolist/ebm-todolist.component';
import { TodolistCardComponent } from './components/todolist-card/todolist-card.component';
import { EbmOnlineComponent } from './ebm-online/ebm-online.component';
import { OnlineTableComponent } from './components/online-table/online-table.component';
import { OnlineTableRowComponent } from './components/online-table-row/online-table-row.component';
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

@NgModule({
  imports: [
    CommonModule,
    EbmZoneRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    ProgressbarModule.forRoot(),
    BsDatepickerModule.forRoot(),
    SharedModule,
    InfiniteScrollModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot()
  ],
  declarations: [
    EbmZoneComponent,
    EbmWorkingComponent,
    EbmScheduleComponent,
    EbmMemoComponent,
    EbmTodolistComponent,
    TodolistCardComponent,
    EbmOnlineComponent,
    OnlineTableComponent,
    OnlineTableRowComponent,
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
    BaseServerPagingTableComponent
  ]
})
export class EbmZoneModule { }
