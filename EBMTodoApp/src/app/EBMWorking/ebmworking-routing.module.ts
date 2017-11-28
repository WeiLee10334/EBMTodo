import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmworkingComponent } from './ebmworking.component';

const routes: Routes = [
  { path: "working", component: EbmworkingComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmworkingRoutingModule { }
