import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EbmonlineComponent } from './ebmonline.component';
import { EbmonlinelistComponent } from './ebmonlinelist/ebmonlinelist.component';

const routes: Routes = [
  {
    path: "", component: EbmonlineComponent, children: [
      { path: "", component: EbmonlinelistComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EbmonlineRoutingModule { }
