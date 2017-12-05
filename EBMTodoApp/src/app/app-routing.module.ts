import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: "working", loadChildren: './ebmworking/ebmworking.module#EbmworkingModule'
    //canLoad: [AuthGuard],
  },
  {
    path: "memo", loadChildren: './memo/memo.module#MemoModule'
    //canLoad: [AuthGuard],
  },
  {
    path: "schedule", loadChildren: './ebmschedule/ebmschedule.module#EbmscheduleModule'
    //canLoad: [AuthGuard],
  },
  {
    path: "todolist", loadChildren: './ebmtodolist/ebmtodolist.module#EbmtodolistModule'
    //canLoad: [AuthGuard],
  },
  { path: '**', redirectTo: 'working' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
