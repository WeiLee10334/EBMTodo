import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/components';
import { DataStoreService } from './shared/services';
import { ExternalPaginationDirective } from './shared/directives/external-pagination.directive';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ExternalPaginationDirective
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
