import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EbmonlineComponent } from './ebmonline.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules';
import { ProgressbarModule, BsDatepickerModule } from 'ngx-bootstrap';
import { EbmonlineRoutingModule } from './ebmonline-routing.module';
import { EbmonlinelistComponent } from './ebmonlinelist/ebmonlinelist.component';

@NgModule({
  imports: [
    CommonModule,
    EbmonlineRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ProgressbarModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  declarations: [
    EbmonlineComponent,
    EbmonlinelistComponent
  ]
})
export class EbmonlineModule { }
