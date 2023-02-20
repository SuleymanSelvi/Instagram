import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  constructor(private services: ServicesService) { }

  loginError: any

  ngOnInit(): void {
    this.services.getToken()

    const session: any = localStorage.getItem("loginSession");
    if (session != null) {
      location.href = "/Home"
    }
  }

  clickLogin(name: any, password: any) {
    this.services.login(name, password).then((res: any) => {
      localStorage.setItem("loginSession", JSON.stringify(res.data))
      location.href = "/Home"
    });
  }

}
