import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';
import { io, Socket } from 'socket.io-client'
import * as $ from 'jquery';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
})
export class PostsComponent implements OnInit {

  postsList: any;
  isLogin: any;
  commentDiv: boolean = false

  // POPUP
  postDetail: any
  postDetailComments: any

  // loader: boolean = false
  userList: any

  socket: Socket
  constructor(private services: ServicesService) {
    this.socket = io("http://localhost:5000/")
  }

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);
    const loginSession: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(loginSession)

    this.socket.connect()

    // this.loader = true
    // setTimeout(() => {
    this.services.getPostsByFollowId(0, 10, session.id).then((res: any) => {
      this.postsList = res.dataList;
    }).catch((res: any) => {
      this.postsList = res.success
      // this.loader = false
    });
    // },1000)

    this.getMyFollow();
  }

  //POPUP POST DETAIL

  displayStyle: any
  blurStyle: any
  openPopup() {
    this.displayStyle = "block";
    this.blurStyle = "blur(10px)";
  }

  video: any = null
  closePopup() {
    // POPUPDA VİDEOYU KAPATINCA VİDEOYU DURDURMA
    if (this.postDetail.images.endsWith('.mp4')) {
      this.video = document.querySelector("#video")
      this.video.pause()
      this.video.currentTime = 0;
    }

    this.displayStyle = "none";
    this.blurStyle = "blur(0px)";
  }

  selectedPost(postId: any) {
    this.openPopup()
    this.services.getPostDetail(postId, this.isLogin.id).then((res: any) => {
      if (res.success) {
        this.postDetail = res.data
      }
    })

    this.services.getPostComments(postId).then((res: any) => {
      if (res.success) {
        this.postDetailComments = res.dataList
      }
    })
  }

  deletePostComment(postId: any, postCommentId: any) {
    this.services.deletePostComment(postCommentId).subscribe((res: any) => {
      if (res.success) {
        this.postDetail.commentCount = res.count

        this.services.getPostComments(postId).then((res: any) => {
          if (res.success) {
            this.postDetailComments = res.dataList
          }
        })

      }
    })
  }

  // POSTS

  postLike(postId: any, userId: any, postOwnerId: any) {
    this.services.uploadpostLike(postId, userId, postOwnerId).then((res: any) => {
      if (res.success && res.data != null) {
        this.postsList.find((x: any) => x.id == postId).existUserLike = res.data.isLike;
        this.postsList.find((x: any) => x.id == postId).likeCount = res.data.likeCount;

        // POPUP POST DETAİL
        this.postDetail.existUserLike = res.data.isLike;
        this.postDetail.likeCount = res.data.likeCount;
      }
    });
  }

  uploadComment(postId: any, userId: any, comment: any, subComment: any) {
    this.services.uploadComment(postId, userId, comment, subComment).then((res: any) => {

      if (res.message == "Yorum Eklendi") {
        this.postsList.find((x: any) => x.id == postId).commentCount = res.count;

        // POPUP POST DETAİL
        this.postDetail.commentCount = res.count;

        this.services.getPostComments(postId).then((res: any) => {
          if (res.success) {
            this.postDetailComments = res.dataList
          }
        })
      }

    })
  }

  savePost(postId: any) {
    var session: any = localStorage.getItem("loginSession")
    session = JSON.parse(session)

    this.services.savePost(postId, session.id).then((res: any) => {
      if (res.success) {
        this.postsList.find((x: any) => x.id == postId).existSaved = res.data.isSaved

        // POPUP POST DETAİL
        this.postDetail.existSaved = res.data.isSaved;
      }
    });
  }

  commentDivShowHide() {
    this.commentDiv = !this.commentDiv
  }

  //DM
  displayStylex: any
  selectedSendPost: any
  openPostDmPopup(posts: any) {
    this.selectedSendPost = posts // İç içe *ngFor olduğu için bu yol
    this.displayStylex = "block"
    // $(".content-body").css({'filter' : 'blur(8px)'})
  }

  closePostDmPopup() {
    this.displayStylex = "none";
  }

  getMyFollow() {
    this.services.getUserFollowing(this.isLogin.id).then((res: any) => {
      this.userList = res.dataList
    })
  }

  timeout: any = null;
  searchUser(userName: any) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      this.services.searchUser(userName).then((res: any) => {
        if (res.success) {
          this.userList = res.dataList
        }
        else if (userName.length == 0) {
          this.userList = this.getMyFollow()
        }
      })
    }, 300);
  }

  sendPostDM(toUserId: any) {
    this.socket.emit("sendPostDM", {
      from: this.isLogin.id,
      to: toUserId,
      postId: this.selectedSendPost.id,
      messageFile: this.selectedSendPost.images,
      messageDescription: this.selectedSendPost.description,
      createdDate: this.selectedSendPost.createdDate,
      userName: this.selectedSendPost.userName,
      userImage: this.selectedSendPost.userImage,
      messageType: "post"
    })

    location.href = "/Direct";
    this.closePostDmPopup();
  }
}