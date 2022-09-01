import { Component, OnInit } from '@angular/core';
import { User } from '../Models/User.Model';
import { TweetsService } from '../tweets.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup,Validators } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  submitted = false;

  constructor(private tweetsService:TweetsService,private router:Router,private fb: FormBuilder) { }

  ngOnInit(): void {    
    
  }
  
  registerForm=new FormGroup({
    first_name:new FormControl('',[Validators.required]),
    last_name:new FormControl(''),
    contactNumber:new FormControl('',[Validators.required]),
    email:new FormControl('',[Validators.required,Validators.email]),
    loginID:new FormControl('',[Validators.required]),
    password:new FormControl('',[Validators.required]),
    confirmPassword:new FormControl('',[Validators.required]),
  })


  get first_name()
  {
    return this.registerForm.get('first_name');
  }
  get contactNumber()
  {
    return this.registerForm.get('contactNumber');
  }
  get email()
  {
    return this.registerForm.get('loginID');
  }
   get loginID()
   {
     return this.registerForm.get('loginID');
   }
  get password()
  {
    return this.registerForm.get('password');
  }
  get confirmPassword()
  {
    return this.registerForm.get('confirmPassword');
  }

  Register(user:User)
  {
    if(user.password==user.confirmPassword)
    {
      if(user.contactNumber.length==10){
        this.tweetsService.registerUser(user);
        alert("Created Successfully!");
        this.router.navigate(['/login']);
      }
      else{
        alert("Contact Number Should be 10 Digits only")
      }
    }
    else{
      alert("Password Not Matched");
    }    
  }

}