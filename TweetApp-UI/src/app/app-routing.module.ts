import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddTweetComponent } from './add-tweet/add-tweet.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TweetsComponent } from './tweets/tweets.component';
import { UsersComponent } from './users/users.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { UpdateTweetComponent } from './update-tweet/update-tweet.component';

const routes: Routes = [
  { path: 'add-tweet', component: AddTweetComponent},
  { path: '', component: LoginComponent},
  { path: 'add-tweet/:name', component: AddTweetComponent},
  { path: 'update-tweet/:id', component: UpdateTweetComponent},
  { path: 'update-tweet', component: UpdateTweetComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {path:'tweets',component:TweetsComponent},
  {path:'tweets/:id',component:TweetsComponent},
  {path:'users',component:UsersComponent},
  {path:"forgotpassword",component:ForgotPasswordComponent}

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
