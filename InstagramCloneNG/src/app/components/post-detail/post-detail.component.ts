import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  isLogin: any
  postDetail: any
  postId: any
  postCommets: any

  ngOnInit(): void {
    const session: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(session)
    this.postId = this.route.snapshot.paramMap.get('id');

    this.getPostDetail();
    this.getPostComments();
  }

  getPostDetail() {
    this.services.getPostDetail(this.postId, this.isLogin.id).then((res: any) => {
      if (res.data != null && res.success) {
        this.postDetail = res.data
      }
    });
  }

  getPostComments() {
    this.services.getPostComments(this.postId).then((res: any) => {
      if (res.success && res.dataList != null) {
        this.postCommets = res.dataList
      }
    });
  }

  postLike(postId: any, userId: any, postOwnerId: any) {
    this.services.uploadpostLike(postId, userId, postOwnerId).then((res: any) => {
      if (res.success && res.data != null) {
        this.postDetail.existUserLike = res.data.isLike;
        this.postDetail.likeCount = res.data.likeCount;
      }
    });
  }

  uploadComment(postId: any, userId: any, comment: any, subComment: any) {
    this.services.uploadComment(postId, userId, comment, subComment).then((res: any) => {
      if (res.success) {
        console.log(res.data.comment)
        this.postDetail.commentCount = res.count;
        // this.postCommets.push(res.data.comment);
        this.getPostComments();
      }
    });
  }

  deletePostComment(postCommentId: any) {
    console.log(postCommentId)
    this.services.deletePostComment(postCommentId).subscribe((res: any) => {
      if (res.success) {
        this.getPostComments();
      }
    })
  }

  savePost(postId: any) {
    var session: any = localStorage.getItem("loginSession")
    session = JSON.parse(session)

    this.services.savePost(postId, session.id).then((res: any) => {
      if (res.success) {
        this.postDetail.existSaved = res.data.isSaved
      }
    });
  }

}
