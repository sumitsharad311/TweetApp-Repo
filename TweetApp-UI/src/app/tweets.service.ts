import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Comments } from './Models/Comments.Model';
import { Tweet } from './Models/Tweet.Model';
import { User } from './Models/User.Model';

@Injectable({
  providedIn: 'root'
})

export class TweetsService {

  private httpHeaders: HttpHeaders;  

  constructor(private http: HttpClient) {
    this.httpHeaders=new HttpHeaders({'Access-Control-Allow-Origin': '*' });
   }
   response:string;
   url:string='https://localhost:44385/api/v1.0';

   loginUser(user:User)
   {
      return this.http.post(this.url+"/users/login",user,{responseType: 'text',observe: 'response'})
   }
   
  registerUser(user:User)
  {
    return this.http.post(this.url+"/users/register",user).subscribe(data=>{})
  }
  forgotpassword(user:User)
  {
    return this.http.put(this.url+"/users/"+user.email+"/forgot/",user)
  }

  validateUser(userName:string):Observable<User>
  {
    const httpParams=new HttpParams({
      fromObject:{
        userName:userName
      }
    }); 
    return this.http.get<User>(this.url+"/users/search/username",{params:httpParams})
  }

  fetchMyTweets(userName:string):Observable<Tweet[]>
  {
    const httpParams=new HttpParams({
      fromObject:{
        userName:userName
      }
    });
    return this.http.get<Tweet[]>(this.url+"/tweets/username",{params:httpParams});
  }
  
  public addTweet(tweet:Tweet)
  {
    this.http.post(this.url+"/tweets/"+tweet.postedBy+"/add",tweet)
    .subscribe(data => {      
    })
  } 
  
  updateTweet(tweet:Tweet)
  {
    return this.http.put(this.url+"/tweets/"+tweet.postedBy+"/update/"+tweet.tweetid,tweet);
    
  }
  replyTweet(user:string,tweetid:string,comment:string)
  {
    var userName=sessionStorage.getItem("loginID");
    if(userName==null)
      userName="";
    const reply:Comments={replyid:userName,repliedBy:userName,comment:comment}
    return this.http.post(this.url+"/tweets/"+user+"/reply/"+tweetid,reply);
  }
  
  likeTweet(tweetid:string,postedBy:string)
  { 
    return this.http.put(this.url+"/tweets/"+postedBy+"/like/"+tweetid,{});    
  }

  deleteTweet(PostedBy:string,tweetid:string)
  {
    this.http.delete(this.url+"/tweets/"+PostedBy+"/delete/"+tweetid).subscribe(data=>{})
  }
  
  fetchTweet():Observable<Tweet[]>{
    return this.http.get<Tweet[]>(this.url+'/tweets/all');
  }

  getUsers():Observable<User[]>{
    return this.http.get<User[]>(this.url+"/users/all");
  }
}
