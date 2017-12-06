import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EbmtodolistComponent } from './ebmtodolist.component';
import { EbmtodolistRoutingModule } from './ebmtodolist-routing.module';
import { EbmtodolistListComponent } from './ebmtodolist-list/ebmtodolist-list.component';
import { CreateTodolistComponent } from './create-todolist/create-todolist.component';
import { SharedModule } from '../shared/modules';

@NgModule({
  imports: [
    CommonModule,
    EbmtodolistRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    EbmtodolistComponent,
    EbmtodolistListComponent,
    CreateTodolistComponent
  ]
})
export class EbmtodolistModule { }
