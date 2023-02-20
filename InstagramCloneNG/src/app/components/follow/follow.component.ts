import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-follow',
  templateUrl: './follow.component.html',
})
export class FollowComponent implements OnInit {

  userList: any;

  constructor(private services: ServicesService) { }

  ngOnInit(): void {
    this.services.getFollow().subscribe(res => {
      this.userList = res;
    });
  }

  // searchUser(userName: any) {
  //   this.services.searchUser(userName).then((res: any) => {
  //     if (res.success) {
  //       this.userList = res.dataList
  //     }
  //   });
  // }
  
}
