import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'ngx-bootstrap';
import { EbmworkingComponent } from './ebmworking.component';
import { EbmworkingRoutingModule } from './ebmworking-routing.module';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { GroupbytimeComponent } from './groupbytime/groupbytime.component';
import { GroupbynameComponent } from './groupbyname/groupbyname.component';
import { PaginationDirective } from '../shared/directives';
import { TableViewComponent } from './table-view/table-view.component';

@NgModule({
  imports: [
    CommonModule,
    EbmworkingRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot()
  ],
  declarations: [
    EbmworkingComponent,
    GroupbytimeComponent,
    GroupbynameComponent,
    PaginationDirective,
    TableViewComponent
  ]
})
export class EbmworkingModule { }
