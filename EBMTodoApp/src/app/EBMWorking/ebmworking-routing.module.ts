import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmworkingComponent } from './ebmworking.component';
import { EbmworkingListComponent } from './components/ebmworking-list/ebmworking-list.component';

const routes: Routes = [
  {
    path: "", component: EbmworkingComponent, children: [
      { path: "", component: EbmworkingListComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmworkingRoutingModule { }
