import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { AppComponent } from './app.component';
import { TweetsComponent } from './tweets/tweets.component';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { AddTweetComponent } from './add-tweet/add-tweet.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AppRoutingModule } from './app-routing.module';
import { UsersComponent } from './users/users.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { UpdateTweetComponent } from './update-tweet/update-tweet.component';
import { ReactiveFormsModule } from  '@angular/forms';



@NgModule({
  declarations: [
    AppComponent,
    TweetsComponent,
    NavbarComponent,
    AddTweetComponent,
    LoginComponent,
    RegisterComponent,
    UsersComponent,
    NavBarComponent,
    ForgotPasswordComponent,
    UpdateTweetComponent  
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    AppRoutingModule,
    ReactiveFormsModule  
    
  ],
  providers: [BsModalService,LoginComponent,TweetsComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
