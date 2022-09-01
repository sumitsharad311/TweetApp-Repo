import { Component } from '@angular/core';
import { BsModalRef } from "ngx-bootstrap/modal";
import { LoginComponent } from './login/login.component';
import { Tweet } from './Models/Tweet.Model';
import { User } from './Models/User.Model';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TweetApp';
  public tweets:Tweet[];
  bsModalRef: BsModalRef;
  

  constructor(private loginComponent:LoginComponent)
  {

  }
  
  ngOnInit(): void {    
    
  }

  user:User;

  getTweets()
  {  
     this.loginComponent.Login(this.user);
  }

}
