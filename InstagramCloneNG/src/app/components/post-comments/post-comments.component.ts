import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-post-comments',
  templateUrl: './post-comments.component.html'
})
export class PostCommentsComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  postCommets: any
  postId: any

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('id');
    this.services.getPostComments(this.postId).then((res: any) => {
      if (res.success && res.dataList != null) {
        this.postCommets = res.dataList
      }
    });
  }

}
