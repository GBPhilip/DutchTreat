import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productLisatView.component';

@NgModule({
  declarations: [
      AppComponent,
      ProductListView 
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
  providers: [Store],
  bootstrap: [AppComponent]
})
export class AppModule { }
