import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Forgotpassword } from '../Models/Forgot.Model';
import { User } from '../Models/User.Model';
import { TweetsService } from '../tweets.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  constructor(private tweetsService:TweetsService,private router:Router) { }

  ngOnInit(): void {
  }
  response:string;

  forgotpassword(user:User)
  
  {
    if(user.password==user.confirmPassword)
    {
      this.tweetsService.forgotpassword(user).subscribe(data=>{this.response=data.toString()});  
      alert("Updated Successfully.");
      this.router.navigate(['/login']); 
    }
    else{
      alert("Password Not Matched");
    }
     
  }

}
