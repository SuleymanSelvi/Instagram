import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  constructor(private services: ServicesService) { }

  isLogin: any
  errorMessage: any

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(session)
  }

  file: any
  getFile(event: any) {
    this.file = <File>event.target.files[0];
  }

  updateProfile(name: any, password: any, email: any, about: any) {
    let fileData = new FormData();
    fileData.append('userId', this.isLogin.id);
    fileData.append('name', name);
    fileData.append('password', password);
    fileData.append('image', this.file);
    fileData.append('email', email);
    fileData.append('about', about);

    this.services.updateProfile(fileData).subscribe((res: any) => {
      if (res.success) {
        alert("İşlem başarılı")
        localStorage.setItem("loginSession", JSON.stringify(res.data))
      }
      else if (!res.success) {
        this.errorMessage = res.message
      }
    })
  }
}
