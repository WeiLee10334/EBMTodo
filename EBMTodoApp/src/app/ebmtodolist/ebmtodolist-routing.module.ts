import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmtodolistComponent } from './ebmtodolist.component';
import { EbmtodolistListComponent } from './ebmtodolist-list/ebmtodolist-list.component';
const routes: Routes = [
  {
    path: "", component: EbmtodolistComponent, children: [
      { path: "", component: EbmtodolistListComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmtodolistRoutingModule { }
