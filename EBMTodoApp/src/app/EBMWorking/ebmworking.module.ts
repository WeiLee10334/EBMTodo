import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'ngx-bootstrap';
import { EbmworkingComponent } from './ebmworking.component';
import { EbmworkingRoutingModule } from './ebmworking-routing.module';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { SharedModule } from '../shared/modules';

@NgModule({
  imports: [
    CommonModule,
    EbmworkingRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    SharedModule
  ],
  declarations: [
    EbmworkingComponent
  ]
})
export class EbmworkingModule { }
