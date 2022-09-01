import { Component, OnInit } from '@angular/core';
import {TweetsComponent} from 'src/app/tweets/tweets.component';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private tweetsComp:TweetsComponent) { }

  ngOnInit(): void {
  }

  Home()
  {
    window.location.reload();
  }

}
