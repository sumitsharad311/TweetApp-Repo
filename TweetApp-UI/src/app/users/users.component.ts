import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../Models/User.Model';
import { TweetsService } from '../tweets.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  
  constructor(private tweetsService:TweetsService,private router:Router) { }
  isLoggedIn=sessionStorage.getItem("isLoggedIn");
  users:User[];

  ngOnInit(): void {
    if(this.isLoggedIn=="False")
    {
      this.router.navigate(['/login']);
      alert("Please Login first");
    }
    else
      this.getUsers();
  }

  getUsers()
  {
    this.tweetsService.getUsers().subscribe(data=>{
      this.users = data;
    });
  }

}
