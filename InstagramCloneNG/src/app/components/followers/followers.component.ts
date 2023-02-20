import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.scss']
})
export class FollowersComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  userList: any
  userId: any

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id');
    this.services.getUserFollowers(this.userId).then((res: any) => {
      this.userList = res.dataList
    })
  }
}
