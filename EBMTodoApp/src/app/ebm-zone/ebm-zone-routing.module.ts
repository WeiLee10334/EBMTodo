import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmZoneComponent } from './ebm-zone.component';
import { EbmWorkingComponent } from './ebm-working/ebm-working.component';
import { EbmScheduleComponent } from './ebm-schedule/ebm-schedule.component';
import { EbmMemoComponent } from './ebm-memo/ebm-memo.component';
import { EbmTodolistComponent } from './ebm-todolist/ebm-todolist.component';
import { EbmOnlineComponent } from './ebm-online/ebm-online.component';

const routes: Routes = [
  {
    path: "", component: EbmZoneComponent, children: [
      { path: "working", component: EbmWorkingComponent },
      { path: "schedule", component: EbmScheduleComponent },
      { path: "memo", component: EbmMemoComponent },
      { path: "todolist", component: EbmTodolistComponent },
      { path: "online", component: EbmOnlineComponent }
    ]
  },
  { path: '**', redirectTo: 'working' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmZoneRoutingModule { }
