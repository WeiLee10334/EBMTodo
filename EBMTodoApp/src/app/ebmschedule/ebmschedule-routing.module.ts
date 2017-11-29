import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmscheduleComponent } from './ebmschedule.component';

const routes: Routes = [
  { path: "", component: EbmscheduleComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmscheduleRoutingModule { }
