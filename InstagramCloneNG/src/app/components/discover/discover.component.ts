import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-discover',
  templateUrl: './discover.component.html',
  styleUrls: ['./discover.component.scss']
})
export class DiscoverComponent implements OnInit {

  constructor(private services : ServicesService) { }

  postsList: any;

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    session = JSON.parse(session);

    this.services.getTopPost(0, 20,session.id).then((res: any) => {
      this.postsList = res.dataList;
    });
  }

}
