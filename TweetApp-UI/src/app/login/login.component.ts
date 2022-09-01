import { Component, OnInit } from '@angular/core';
import { Tweet } from '../Models/Tweet.Model';
import { User } from '../Models/User.Model';
import { TweetsService } from '../tweets.service';
import { TweetsComponent } from '../tweets/tweets.component';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private tweetsService :TweetsService,private tweetsComponent:TweetsComponent,private router: Router,private route:ActivatedRoute) { }
  isLoggedIn=sessionStorage.getItem("isLoggedIn");
  response=sessionStorage.getItem("loginID");

  loginForm=new FormGroup({
    loginID:new FormControl('',[Validators.required]),    
    password:new FormControl('',[Validators.required])
  })
  get loginID()
  {
    return this.loginForm.get('loginID');
  }
  get password()
  {
    return this.loginForm.get('password');
  }

  ngOnInit(): void {  
    if(this.isLoggedIn=="True")
    {
      alert("You are already logged in");
      
      this.router.navigate(['/add-tweet/'+this.response]);
    }
  }
  res:string;
  users:User;
  statusCode:number;
  Login(user:User)
  {
    this.res=user.loginID;
    this.tweetsService.loginUser(user).subscribe(result=>{
      console.log(result.status);
      this.statusCode=result.status;
    
      if(this.statusCode==200)
      {
        sessionStorage.setItem("isLoggedIn","True");
        sessionStorage.setItem("loginID",user.loginID);
        this.router.navigate(['/add-tweet/'+user.loginID]);        
      }      
         
    },error=>{
      console.log(error.status);
      this.statusCode=error.status;
    
      if(this.statusCode==404)
      {
        alert("Wrong Username or Password.");
        this.router.navigate(['/login']);       
      } 
    })  
  }
  createUser()
  {
    this.router.navigate(['/register']);
  }

}
