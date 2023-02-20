import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.scss']
})
export class FollowingComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  userList: any
  userId: any

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id');
    this.services.getUserFollowing(this.userId).then((res: any) => {
      this.userList = res.dataList
    })
  }
}
