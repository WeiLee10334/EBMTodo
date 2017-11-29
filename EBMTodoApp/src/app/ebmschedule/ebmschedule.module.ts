import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'ngx-bootstrap';
import { EbmscheduleRoutingModule } from './ebmschedule-routing.module';
import { EbmscheduleComponent } from './ebmschedule.component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { GroupbytimeComponent } from './groupbytime/groupbytime.component';
import { GroupbynameComponent } from './groupbyname/groupbyname.component';
import { TableViewComponent } from './table-view/table-view.component';
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
    EbmscheduleComponent,
    GroupbytimeComponent,
    GroupbynameComponent,
    TableViewComponent
  ]
})
export class EbmscheduleModule { }
