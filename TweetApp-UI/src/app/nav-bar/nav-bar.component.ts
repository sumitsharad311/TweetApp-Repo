import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private router:Router) { }

  response=sessionStorage.getItem("email");

  ngOnInit(): void {
    this.response=sessionStorage.getItem("email");
  }
  getUser()
  {
    this.response=sessionStorage.getItem("email");
    this.router.navigateByUrl('add-tweet/'+this.response);
  }

  logout()
  {
    sessionStorage.setItem("email","");
    sessionStorage.setItem("isLoggedIn","False");
    this.router.navigate(['/login']);
  }
}
