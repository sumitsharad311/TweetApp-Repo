import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Tweet } from '../Models/Tweet.Model';
import { User } from '../Models/User.Model';
import { TweetsService } from '../tweets.service';
import { TweetsComponent } from '../tweets/tweets.component';
import { UpdateTweetComponent } from '../update-tweet/update-tweet.component';

@Component({
  selector: 'app-add-tweet',
  templateUrl: './add-tweet.component.html',
  styleUrls: ['./add-tweet.component.css']
})
export class AddTweetComponent implements OnInit {
  
  remainChar:number=144;
  modalRef: BsModalRef;
  public tweets:Tweet[];
  constructor(private tweetsService:TweetsService,private route: ActivatedRoute,private router: Router,private modalService: BsModalService) { }
  userName:string="";
  tweetId:string="";
  tweetBody:string="";
  isLoggedIn=sessionStorage.getItem("isLoggedIn");

  ngOnInit(): void {    
    this.userName=this.route.snapshot.params["name"];
    this.tweetId=this.route.snapshot.params["id"];
    if(this.isLoggedIn=="False")
    {
      this.router.navigate(['/login']);
      alert("Please Login first");
    }
    this.getMytweets();
    this.getUser();    
  }
  setRemain(value:NgForm)
  {
    this.remainChar=144-value.value['tweet']?.length;
  }
  goToUpdate(tweetid:string,tweet:string)
  {
    sessionStorage.setItem("tweetBody",tweet);
    this.router.navigate(['/update-tweet/'+tweetid]);
  }
  deleteTweet(tweetid:string)
  {
    this.tweetsService.deleteTweet(this.userName,tweetid);
    window.location.reload();
  } 
  onCreateTweet(tweet:Tweet)
  {    
    tweet.postedBy=this.userName;
    this.tweetsService.addTweet(tweet);
    window.location.reload();
  }
  getMytweets()
  {
    this.tweetsService.fetchMyTweets(this.userName).subscribe(data=>{this.tweets=data});
  }

  onUpdateTweet(tweet:Tweet)
  {
    this.modalRef = this.modalService.show(UpdateTweetComponent);
  }
  user:User;
  getUser()
  {
    this.tweetsService.validateUser(this.userName).subscribe(data=>{console.log(data);this.user=data});
  }
}
