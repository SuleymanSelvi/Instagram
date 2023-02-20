import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {

  constructor(private services: ServicesService) { }

  registirError: any

  ngOnInit(): void {
  }

  clickRegister(name: string, password: string, email: string, about: string) {
    this.services.register(name, password, email, about).then((res:any)=>{
      localStorage.setItem("loginSession", JSON.stringify(res.data))
      location.href = "/Home"
    });
  }

}
