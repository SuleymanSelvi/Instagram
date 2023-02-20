import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-remove',
  templateUrl: './remove.component.html',
  styleUrls: ['./remove.component.scss']
})
export class RemoveComponent implements OnInit {

  constructor(private services: ServicesService) { }

  isLogin: any

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(session)
  }

  deleteAccount(password: any) {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session)

    this.services.deleteAccount(session.id, password).subscribe((res: any) => {
      if (res.success) {
        localStorage.clear();
        location.href = "/"
      }
    })
  }

  displayStyle: any
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }

}
