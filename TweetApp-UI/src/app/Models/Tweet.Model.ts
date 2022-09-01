import { Comments } from "./Comments.Model";

export class Tweet{
     tweetid:string;
     tweet: string;
     like: number;
     postedBy: string;
     tweetcomments:Comments[];
     change:boolean=false;
}