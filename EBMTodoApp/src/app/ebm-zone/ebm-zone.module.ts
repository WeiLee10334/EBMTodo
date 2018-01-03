import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EbmZoneRoutingModule } from './ebm-zone-routing.module';
import { EbmZoneComponent } from './ebm-zone.component';
import { EbmWorkingComponent } from './ebm-working/ebm-working.component';
import { SharedModule } from '../shared/modules/index';
import { AccordionModule, BsDatepickerModule, ProgressbarModule } from 'ngx-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { EbmScheduleComponent } from './ebm-schedule/ebm-schedule.component';
import { EbmMemoComponent } from './ebm-memo/ebm-memo.component';
import { EbmTodolistComponent } from './ebm-todolist/ebm-todolist.component';
import { TodolistCardComponent } from './components/todolist-card/todolist-card.component';
import { EbmOnlineComponent } from './ebm-online/ebm-online.component';
import { OnlineTableComponent } from './components/online-table/online-table.component';
import { OnlineTableRowComponent } from './components/online-table-row/online-table-row.component';
import { EbmProjectComponent } from './ebm-project/ebm-project.component';

@NgModule({
  imports: [
    CommonModule,
    EbmZoneRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    ProgressbarModule.forRoot(),
    BsDatepickerModule.forRoot(),
    SharedModule
  ],
  declarations: [
    EbmZoneComponent,
    EbmWorkingComponent,
    EbmScheduleComponent,
    EbmMemoComponent,
    EbmTodolistComponent,
    TodolistCardComponent,
    EbmOnlineComponent,
    OnlineTableComponent,
    OnlineTableRowComponent,
    EbmProjectComponent,
  ]
})
export class EbmZoneModule { }
