import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-saved-posts',
  templateUrl: './saved-posts.component.html',
  styleUrls: ['./saved-posts.component.scss']
})
export class SavedPostsComponent implements OnInit {

  constructor(private services: ServicesService) { }

  savedPostList: any

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);

    this.services.getSavedPosts(session.id).then((res: any) => {
      if (res.success) {
        this.savedPostList = res.dataList
      }
    })
  }

}
