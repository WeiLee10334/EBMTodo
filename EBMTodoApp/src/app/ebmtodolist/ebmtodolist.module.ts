import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EbmtodolistComponent } from './ebmtodolist.component';
import { EbmtodolistRoutingModule } from './ebmtodolist-routing.module';
import { EbmtodolistListComponent } from './ebmtodolist-list/ebmtodolist-list.component';
import { SharedModule } from '../shared/modules';
import { TodoCardComponent } from './components/todo-card/todo-card.component';
import { ProgressbarModule, BsDatepickerModule } from 'ngx-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    EbmtodolistRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ProgressbarModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  declarations: [
    EbmtodolistComponent,
    EbmtodolistListComponent,
    TodoCardComponent
  ]
})
export class EbmtodolistModule { }
