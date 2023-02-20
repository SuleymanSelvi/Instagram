import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-user-posts',
  templateUrl: './user-posts.component.html',
  styleUrls: ['./user-posts.component.scss']
})
export class UserPostsComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  isLogin: any
  userPosts: any
  postId: any

  ngOnInit(): void {
    const loginSession: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(loginSession)

    this.postId = this.route.snapshot.paramMap.get('id');
    this.services.getUserPosts(this.postId).then((res: any) => {
      if (res.dataList != null && res.success) {
        this.userPosts = res.dataList
      }
    })
  }

  displayStyle: any
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }

  postLike(postId: any, userId: any, postOwnerId: any) {
    this.services.uploadpostLike(postId, userId, postOwnerId).then((res: any) => {
      if (res.success && res.data != null) {
        this.userPosts.find((x: any) => x.id == postId).existUserLike = res.data.isLike;
        this.userPosts.find((x: any) => x.id == postId).likeCount = res.data.likeCount;
      }
    });
  }

  savePost(postId: any) {
    var session: any = localStorage.getItem("loginSession")
    session = JSON.parse(session)

    this.services.savePost(postId, session.id).then((res: any) => {
      if (res.success) {
        this.userPosts.find((x: any) => x.id == postId).existSaved = res.data.isSaved
      }
    });
  }

  deletePost(postId: any) {
    this.services.deletePost(postId).subscribe((res: any) => {
      if (res.success) {

        this.displayStyle = "none";

        this.services.getUserPosts(this.postId).then((res: any) => {
          if (res.dataList != null && res.success) {
            this.userPosts = res.dataList
          }
        })
      }
      else if (!res.success) {
        alert("gg");
      }
    })
  }

}
