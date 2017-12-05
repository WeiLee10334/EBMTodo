import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationDirective } from '../../directives';
import { PagingTableComponent } from '../../components';
@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    PaginationDirective,
    PagingTableComponent
  ],
  exports: [
    PaginationDirective,
    PagingTableComponent
  ]
})
export class SharedModule { }
