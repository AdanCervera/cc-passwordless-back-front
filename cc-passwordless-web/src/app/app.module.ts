import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RequestMagicLinkComponent } from './features/authentication/request-magic-link/request-magic-link.component';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthenticationComponent } from './features/authentication/authentication.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { AccessGrantedComponent } from './features/access-granted/access-granted.component';
import { ContractsComponent } from './features/constracts/contracts.component';
import { MatTableModule } from '@angular/material/table';
@NgModule({
  declarations: [
    AppComponent,
    RequestMagicLinkComponent,
    AuthenticationComponent,
    AccessGrantedComponent,
    ContractsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule, 
    ReactiveFormsModule,
    HttpClientModule,
    MatProgressBarModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
