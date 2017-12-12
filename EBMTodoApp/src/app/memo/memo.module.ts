import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'ngx-bootstrap';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MemoRoutingModule } from './memo-routing.module';
import { MemoComponent } from './memo.component';
import { SharedModule } from '../shared/modules';
import { MemoListComponent } from './components/memo-list/memo-list.component';

@NgModule({
  imports: [
    CommonModule,
    MemoRoutingModule,
    MultiselectDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    SharedModule
  ],
  declarations: [
    MemoComponent,
    MemoListComponent
  ]
})
export class MemoModule { }
