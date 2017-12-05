import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'ngx-bootstrap';
import { EbmscheduleRoutingModule } from './ebmschedule-routing.module';
import { EbmscheduleComponent } from './ebmschedule.component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { SharedModule } from '../shared/modules';

@NgModule({
  imports: [
    CommonModule,
    EbmscheduleRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    SharedModule
  ],
  declarations: [
    EbmscheduleComponent
  ]
})
export class EbmscheduleModule { }
