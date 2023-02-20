import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-liked-posts',
  templateUrl: './liked-posts.component.html',
  styleUrls: ['./liked-posts.component.scss']
})
export class LikedPostsComponent implements OnInit {

  constructor(private services: ServicesService) { }

  likedPosts: any

  ngOnInit(): void {
    let session : any = localStorage.getItem("loginSession");
    session = JSON.parse(session)

    this.services.getLikedPosts(session.id).then((res:any)=> {
      if(res.success){
        this.likedPosts = res.dataList
      }
    })
  }

}
