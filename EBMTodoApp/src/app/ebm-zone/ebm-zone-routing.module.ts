import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmZoneComponent } from './ebm-zone.component';
import { EbmWorkingComponent } from './ebm-working/ebm-working.component';
import { EbmScheduleComponent } from './ebm-schedule/ebm-schedule.component';
import { EbmMemoComponent } from './ebm-memo/ebm-memo.component';
import { EbmTodolistComponent } from './ebm-todolist/ebm-todolist.component';
import { EbmOnlineComponent } from './ebm-online/ebm-online.component';
import { EbmProjectComponent } from './ebm-project/ebm-project.component';
import { EbmProjectMemberComponent } from './ebm-project/ebm-project-member/ebm-project-member.component';
import { EbmProjectWorkingComponent } from './ebm-project/ebm-project-working/ebm-project-working.component';
import { EbmUserComponent } from './ebm-user/ebm-user.component';
import { EbmProjectTodolistComponent } from './ebm-project/ebm-project-todolist/ebm-project-todolist.component';
import { EbmProjectListComponent } from './ebm-project/ebm-project-list/ebm-project-list.component';
import { EbmUserListComponent } from './ebm-user/ebm-user-list/ebm-user-list.component';
import { EbmUserMemberComponent } from './ebm-user/ebm-user-member/ebm-user-member.component';

const routes: Routes = [
  {
    path: "", component: EbmZoneComponent, children: [
      { path: "working", component: EbmWorkingComponent },
      { path: "schedule", component: EbmScheduleComponent },
      { path: "memo", component: EbmMemoComponent },
      { path: "todolist", component: EbmTodolistComponent },
      { path: "online", component: EbmOnlineComponent },
      {
        path: "project", component: EbmProjectComponent, children: [
          { path: "", component: EbmProjectListComponent },
          { path: "member", component: EbmProjectMemberComponent },
          { path: "working", component: EbmProjectWorkingComponent },
          { path: "todolist", component: EbmProjectTodolistComponent },
          { path: "*", redirectTo: "project" }
        ]
      },
      {
        path: "user", component: EbmUserComponent, children: [
          { path: "", component: EbmUserListComponent },
          { path: "member", component: EbmUserMemberComponent },
          { path: "*", redirectTo: "user" }
        ]
      },

    ]
  },
  { path: '**', redirectTo: 'working' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmZoneRoutingModule { }
