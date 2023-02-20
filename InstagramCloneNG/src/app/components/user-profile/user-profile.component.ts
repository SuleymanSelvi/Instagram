import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';
import { Title } from "@angular/platform-browser";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  constructor(private services: ServicesService, private titleService: Title, private route: ActivatedRoute) { }

  userProfile: any;
  userId: any;
  isLogin: any;
  showHide: boolean = false
  tools: boolean = false
  
  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(session)
    session = JSON.parse(session);

    this.userId = this.route.snapshot.paramMap.get('id');
    this.services.getUserProfile(this.userId, session.id).then((res: any) => {
      if (res.data != null && res.success) {
        this.userProfile = res.data;
        this.titleService.setTitle(res.data.name);
      }
    });
  }

  followUser(followerId: any, followId: any) {
    this.services.followUser(followerId, followId).then((res: any) => {
      this.userProfile.isFollow = res.data.isFollow;
      this.userProfile.followCount = res.data.followCount;
    })
  }

  divShow() {
    this.showHide = !this.showHide
  }

  divTools() {
    this.tools = !this.tools
  }

  blockUser(blockedId: any) {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);

    this.services.blockUser(blockedId, session.id).subscribe((res: any) => {
      if (res.success) {
        this.userProfile.isBlock = res.data.isBlock;
        this.userProfile.isFollow = res.data.isFollow
      }
    })
  }
}
