import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor() { }

  isLogin: any

  ngOnInit(): void {
    const loginSession: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(loginSession)

    if (this.isLogin == null) {
      location.href = "/";
    }
  }
}
