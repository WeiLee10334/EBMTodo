import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EbmtodolistComponent } from './ebmtodolist.component';
import { EbmtodolistRoutingModule } from './ebmtodolist-routing.module';

@NgModule({
  imports: [
    CommonModule,
    EbmtodolistRoutingModule
  ],
  declarations: [
    EbmtodolistComponent
  ]
})
export class EbmtodolistModule { }
