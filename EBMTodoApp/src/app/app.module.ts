import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/components';
import { DataStoreService } from './shared/services';
import { PaginationDirective } from './shared/directives';
import { EbmscheduleComponent } from './ebmschedule/ebmschedule.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpModule
  ],
  providers: [
    DataStoreService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
