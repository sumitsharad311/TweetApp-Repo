import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { map } from 'rxjs/operators';
import { Comments } from '../Models/Comments.Model';
import { Tweet } from '../Models/Tweet.Model';
import { TweetsService } from '../tweets.service';
import { UpdateTweetComponent } from '../update-tweet/update-tweet.component';


@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.css']
})

export class TweetsComponent implements OnInit {

  public tweets:Tweet[];
  //public comment:Comments[];
  public modalRef: BsModalRef;
  change:boolean=false;
  


  constructor(private tweetsService : TweetsService,private router:Router,private modalService: BsModalService,private route:ActivatedRoute) {    
   }  
   
  
  isLoggedIn=sessionStorage.getItem("isLoggedIn");
  tweetId:string="";
  userName=sessionStorage.getItem("email");

  Form=new FormGroup({    
    comment:new FormControl('',[Validators.required])
  })

  
  ngOnInit(): void {
    if(this.isLoggedIn!="True")
    {
      this.router.navigate(['/login']);
      alert("Please Login first");
    }
    else
    {
      this.tweetId=this.route.snapshot.params["id"];
      this.getTweets();
    }
      this.getTweets();
  }
  // myFunc()
  // {
  //   //this.change=true;
  //   if(this.change==true)
  //     this.change=false;
  //   else
  //     this.change=true;
  // }
  getTweets()
   {
    this.tweetsService.fetchTweet().subscribe(data=>{this.tweets=data})
    
    //this.comment=this.tweets.tweetcomments[]    
   }

   Reply(tweetid:string,user:string)
   { 
     if(this.userName==null)
        this.userName="";

    this.tweetsService.replyTweet(user,tweetid,this.Form.value.comment).subscribe(data => {      
      sessionStorage.setItem("response","True")
    })    
    window.location.reload();
   }  
  
  name = sessionStorage.getItem("response");
  
  
  tweet:Tweet;
  like(tweetid:string,postedBy:string)
  {    
    this.tweetsService.likeTweet(tweetid,postedBy).subscribe(data=>{});
    
    window.location.reload();
  }

  
}
