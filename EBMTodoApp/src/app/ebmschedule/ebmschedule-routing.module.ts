import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmscheduleComponent } from './ebmschedule.component';
import { EbmscheduleListComponent } from './components/ebmschedule-list/ebmschedule-list.component';

const routes: Routes = [
  {
    path: "", component: EbmscheduleComponent, children: [
      { path: "", component: EbmscheduleListComponent },
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmscheduleRoutingModule { }
