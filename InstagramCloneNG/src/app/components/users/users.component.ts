import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  constructor(private services: ServicesService, private route: ActivatedRoute) { }

  userList: any
  userName: any
  
  ngOnInit(): void {
    this.userName = this.route.snapshot.paramMap.get('id');
     this.services.searchUser(this.userName).then((res: any) => {
     this.userList = res.dataList
    })
  }

  searchUser(userName : any){
    this.services.searchUser(userName).then((res: any) => {
      if (res.success) {
        this.userList = res.dataList
      }
    });
  }
 
}
