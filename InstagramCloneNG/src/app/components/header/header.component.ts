import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';
import * as $ from 'jquery';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private services: ServicesService) { }

  isLogin: any
  userList: any

  notificationsList: any
  notificationsDiv: boolean = false
  isSeen: boolean = false

  divNone: boolean = false
  searchNone: boolean = false
  timeout: any = null;

  ngOnInit(): void {
    const loginSession: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(loginSession)

    this.getNotifications();
    this.jquery();
  }

  jquery() {
    $('#openPopup').on('click', function () {
      $("#showPopup").show()
      $(".content-body").css({ 'filter': 'blur(10px)' });
    });

    $('#closePopup').on('click', function () {
      $("#showPopup").hide()
      $(".content-body").css({ 'filter': 'blur(0px)' });
    });
  }

  // Post Yükleme
  file: any
  imageSrc: any
  getFile(event: any) {
    this.file = <File>event.target.files[0];
    this.imageSrc = URL.createObjectURL(event.target.files[0]);
    $("#img").attr("src", this.imageSrc);
  }

  uploadPost(description: any) {
    let fileData = new FormData();
    fileData.append('userId', this.isLogin.id);
    fileData.append('images', this.file);
    fileData.append('description', description);

    this.services.uploadPost(fileData).subscribe((res: any) => {
      if (res.success) {
        $("#showPopup").hide()
        $(".content-body").css({ 'filter': 'blur(0px)' });
      }
    })
  }

  // Kullanıcı
  logOut() {
    localStorage.clear();
    location.href = "/"
  }

  divOpen() {
    this.divNone = !this.divNone
  }

  // Search 
  searchUser(userName: any) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      this.services.searchUser(userName).then((res: any) => {
        if (res.success) {
          this.userList = res.dataList
          this.searchNone = userName.length > 0 ? true : false

          // this.searchOpen(userName)
        }
      });
    }, 500);
  }

  // searchOpen(userName: any) {
  //   this.searchNone = userName.length != 0
  //  }

  // searchClose() {
  //   this.searchNone = false
  // }

  // Notifications 
  getNotifications() {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);

    this.services.getNotifications(session.id).then((res: any) => {
      if (res.success) {
        this.notificationsList = res.dataList
        this.isSeen = res.optionalBoolean
      }
    })
  }

  updateNotifications() {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);

    this.services.updateNotifications(session.id).then((res: any) => {
      if (res.success) {
        this.isSeen = res.data.isSeen
      }
    })
  }

  searchOpenNotifications() {
    this.notificationsDiv = !this.notificationsDiv
  }
}
