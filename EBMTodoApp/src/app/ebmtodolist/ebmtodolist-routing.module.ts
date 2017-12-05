import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmtodolistComponent } from './ebmtodolist.component';

const routes: Routes = [
  { path: "", component: EbmtodolistComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmtodolistRoutingModule { }
