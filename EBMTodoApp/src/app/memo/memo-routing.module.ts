import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MemoComponent } from './memo.component';

const routes: Routes = [
  { path: "", component: MemoComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MemoRoutingModule { }
