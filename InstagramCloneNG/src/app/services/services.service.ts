import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  constructor(private http: HttpClient) { }

  // callback
  // async -await
  // promise

  InstagramPost(url: any, body?: any) {
    let token: any = localStorage.getItem("token");
    token = JSON.parse(token);
    const httpOptions = {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token
      })
    };
    return new Promise((resolve, reject) => {
      this.http.post(url, body, httpOptions).subscribe((res: any) => {
        if (!res.success) {
         // alert(res.error)
        }
        if (res.success && (res.data != null || res.dataList != null)) {
          resolve(res);
        }
      }, (error) => {
        if (error.status == 401) {  // sistem token almalı
          this.getToken()
          alert("lütfen tekrar deneyiniz")
        }
        else if (error.status == 400) {  // model hatalı

        }
        else if (error.status == 404) {

        }
        //reject(error)
      });
    });
  }

  InstagramGet(url: any, body?: any) {
    let token: any = localStorage.getItem("token");
    token = JSON.parse(token);
    const httpOptions = {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token
      })
    };
    return new Promise((resolve, reject) => {
      this.http.get(url, body).subscribe((res: any) => {
        if (!res.success) {
          alert(res.error)
        }
        if (res.success && (res.data != null || res.dataList != null)) {
          resolve(res);
        }
      }, (error) => {
        if (error.status == 401) {  // sistem token almalı
          this.getToken()
          alert("lütfen tekrar deneyiniz")
        }
        else if (error.status == 400) {  // model hatalı

        }
        else if (error.status == 404) {

        }
        //reject(error)
      });
    });
  }

  getToken() {
    return this.http.post("http://localhost:5122/Token/GetToken", {
      "UserName": "Test",
      "Password": "123123123",
    }).subscribe((res: any) => {
      if (res.success) {
        localStorage.setItem("token", JSON.stringify(res.message))
      }
    });
  }

  login(name: any, password: any) {
    return this.InstagramPost("http://localhost:5122/Users/Login", {
      "name": name,
      "password": password,
      "email": "",
      "about": ""
    })
  }

  register(name: string, password: string, email: string, about: string) {
    return this.InstagramPost("http://localhost:5122/Users/Register", {
      "name": name,
      "password": password,
      "email": email,
      "about": about
    });
  }

  getTopPost(skip: any, take: any, sessionUserId: any) {
    return this.InstagramGet("http://localhost:5122/Post/GetTopPosts?skip=" + skip + "&take=" + take + "&sessionUserId=" + sessionUserId)
  }

  getPostsByFollowId(skip: any, take: any, sessionUserId: any) {
    return this.InstagramGet("http://localhost:5122/Post/GetPostsByFollowId?skip=" + skip + "&take=" + take + "&sessionUserId=" + sessionUserId)
  }

  uploadpostLike(postId: any, userId: any, postOwnerId: any) {
    return this.InstagramPost("http://localhost:5122/PostLike/PostLike?postId=", {
      "postId": postId,
      "userId": userId,
      "postOwnerId": postOwnerId
    });
  }

  uploadComment(postId: any, userId: any, comment: any, subComment: any) {
    return this.InstagramPost("http://localhost:5122/PostComment/UploadComment", {
      "postId": postId,
      "userId": userId,
      "comment": comment,
      "subComment": subComment
    });
  }

  getPostDetail(postId: any, sessionUserId: any) {
    return this.InstagramGet("http://localhost:5122/Post/GetPostDetail?postId=" + postId + "&sessionUserId=" + sessionUserId)
  }

  getPostComments(postId: any) {
    return this.InstagramGet("http://localhost:5122/PostComment/GetPostComments?postId=" + postId)
  }

  getUserProfile(userId: any, sessionUserId: any) {
    return this.InstagramGet("http://localhost:5122/Users/GetUserProfile?userId=" + userId + "&sessionUserId=" + sessionUserId)
  }

  getUserPosts(userId: any) {
    return this.InstagramGet("http://localhost:5122/Post/GetPostsByUserId?userId=" + userId)
  }

  followUser(followId: any, followerId: any) {
    return this.InstagramPost("http://localhost:5122/Followers/FollowUser?followId=", {
      "followId": followId,
      "followerId": followerId
    })
  }
  
  getUserFollowingForChat(userId: any) {
    return this.InstagramGet("http://localhost:5122/Followers/GetUserFollowingForChat?userId=" + userId)
  }

  getUserFollowing(userId: any) {
    return this.InstagramGet("http://localhost:5122/Followers/GetUserFollowing?userId=" + userId)
  }

  getUserFollowers(userId: any) {
    return this.InstagramGet("http://localhost:5122/Followers/GetUserFollowers?userId=" + userId)
  }

  getFollow() {
    return this.http.get("http://localhost:5122/Users/GetUsers")
  }

  searchUser(userName: any) {
    return this.InstagramPost("http://localhost:5122/Users/SearchUsers?userName=", {
      "userName": userName
    })
  }

  updateProfile(fileData: any) {
    return this.http.put("http://localhost:5122/Users/UpdateProfile", fileData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }

  uploadPost(fileData: any) {
    return this.http.post("http://localhost:5122/Post/UploadPost", fileData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }

  deletePost(postId: any) {
    return this.http.delete("http://localhost:5122/Post/DeletePost?postId=" + postId);
  }

  deletePostComment(postCommentId: any) {
    return this.http.delete("http://localhost:5122/PostComment/DeletePostComment?postCommentId=" + postCommentId);
  }

  blockUser(blockedId: any, blockId: any) {
    return this.http.delete("http://localhost:5122/UserBlock/BlockUser?blockedId=" + blockedId + "&blockId=" + blockId)
  }

  savePost(postId: any, userId: any) {
    return this.InstagramPost("http://localhost:5122/SavedPosts/SavedPosts", {
      "postId": postId,
      "userId": userId
    });
  }

  getSavedPosts(userId: any) {
    return this.InstagramGet("http://localhost:5122/SavedPosts/GetSavedPosts?userId=" + userId)
  }

  getLikedPosts(userId: any) {
    return this.InstagramGet("http://localhost:5122/PostLike/GetLikedPosts?userId=" + userId)
  }

  deleteAccount(userId: any, password: any) {
    return this.http.get("http://localhost:5122/Users/DeleteAccount?userId=" + userId + "&password=" + password)
    // return this.http.put("http://localhost:5122/Users/DeleteAccount", {
    //   "userId": userId,
    //   "password": password
    // })
  }

  getNotifications(userId: any) {
    return this.InstagramGet("http://localhost:5122/Notifications/GetNotifications?userId=" + userId)
  }

  updateNotifications(userId: any) {
    return this.InstagramGet("http://localhost:5122/Notifications/UpdateNotifications?userId=" + userId)
  }

  getStorys(sessionId: any) {
    return this.InstagramGet("http://localhost:5122/Story/GetStorys?sessionId=" + sessionId)
  }

  uploadStory(fileData: any) {
    return this.http.post("http://localhost:5122/Story/UploadStory", fileData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }

  uploadStoryView(storyId: any, userId: any) {
    return this.InstagramPost("http://localhost:5122/StoryView/UploadStoryView", {
      "storyId": storyId,
      "userId": userId
    })
  }

  deleteStory(storyId: any) {
    return this.http.delete("http://localhost:5122/Story/DeleteStory?storyId=" + storyId)
  }

  uploadChatImage(fileData: any) {
    return this.http.post("http://localhost:5122/Users/UploadChatImage", fileData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }

}
