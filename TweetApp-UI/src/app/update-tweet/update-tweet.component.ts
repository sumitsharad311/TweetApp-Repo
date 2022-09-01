import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tweet } from '../Models/Tweet.Model';
import { TweetsService } from '../tweets.service';

@Component({
  selector: 'app-update-tweet',
  templateUrl: './update-tweet.component.html',
  styleUrls: ['./update-tweet.component.css']
})
export class UpdateTweetComponent implements OnInit {

  constructor(private route:ActivatedRoute,private tweetsService:TweetsService,private router:Router) { }

  tweetId:string="";
  name:string="";
  userName=sessionStorage.getItem("loginID");
  tweet=sessionStorage.getItem("tweetBody");


  ngOnInit(): void {
    this.tweetId=this.route.snapshot.params["id"];
    //this.name=this.route.snapshot.params["name"];
  }

  onUpdateTweet(tweet:Tweet)
  {
    //const tweets:Tweet={tweet:"",postedBy:postedBy,like:0,tweetid:tweetid}
    this.tweetsService.updateTweet(tweet).subscribe(data => {      
      sessionStorage.setItem("response","True")
    })    
    //document.getElementById("closeModalButton")?.click();
    this.router.navigateByUrl('add-tweet/'+this.userName).then(()=>{window.location.reload()});    
  }

}
