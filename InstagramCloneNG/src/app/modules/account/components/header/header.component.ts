import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private services: ServicesService) { }

  isLogin: any
  userList: any
  divNone : boolean = false
  searchNone: boolean = false

  ngOnInit(): void {
    const loginSession: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(loginSession)
  }
  
  logOut() {
    localStorage.clear();
    location.href = "/"
  }

  divOpen(){
    this.divNone = !this.divNone
  }

  searchUser(userName: any) {
    this.services.searchUser(userName).then((res: any) => {
      if (res.success) {
        this.userList = res.dataList
      }
    });
  }

  searchOpen() {
    this.searchNone = !this.searchNone
  }


}
