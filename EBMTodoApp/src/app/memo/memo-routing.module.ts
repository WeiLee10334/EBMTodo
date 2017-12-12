import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MemoComponent } from './memo.component';
import { MemoListComponent } from './components/memo-list/memo-list.component';

const routes: Routes = [
  {
    path: "", component: MemoComponent, children: [
      { path: "", component: MemoListComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MemoRoutingModule { }
