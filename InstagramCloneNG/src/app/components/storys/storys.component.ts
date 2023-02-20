import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';
import * as $ from 'jquery';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-storys',
  templateUrl: './storys.component.html',
  styleUrls: ['./storys.component.scss']
})
export class StorysComponent implements OnInit {

  constructor(private services: ServicesService, private sanitizer: DomSanitizer) { }

  isLogin: any
  storysList: any
  storyDetail: any

  ngOnInit(): void {
    let session: any = localStorage.getItem("loginSession");
    this.isLogin = JSON.parse(session)

    this.services.getStorys(this.isLogin.id).then((res: any) => {
      if (res.success) {
        this.storysList = res.dataList
      }
    })

    this.jquery();
  }

  jquery() {

    // STORYS
    // $('#openStory').on('click', function () {
    //   $("#storyPopup").css({ 'display': 'flex' });
    // });

    //  $('#closeStory').on('click', function () {
    //    $("#storyPopup").css({ 'display': 'none' });
    //  });

    // UPLOAD STORY
    $('#uploadStoryDiv').on('click', function () {
      $("#uploadStoryShow").css({ 'display': 'flex' });
    });

    $('#uploadStoryHide').on('click', function () {
      $("#uploadStoryShow").css({ 'display': 'none' });
    });

  }

  displayStyle: any
  blurStyle: any
  openPopup() {
    this.displayStyle = "block";
  }

  video: any = null
  closePopup() {
    // STORYDE VİDEOYU KAPATINCA VİDEOYU DURDURMA
    if (this.storyDetail.images.endsWith('.mp4')) {
      this.video = document.querySelector("#video")
      this.video.pause()
      this.video.currentTime = 0;
    }

    this.displayStyle = "none";
  }

  timeout: any = null;
  barWidth = 0;
  selectedUserStorys: any
  floorStoryTime: any
  selectedStory(userId: any) {

    // KULLANICI BU STORY'İ İZLEDİ
    // this.services.uploadStoryView(storyId, this.isLogin.id).then((res: any) => {
    //   if (res.success) {
    //     this.storysList.find((x: any) => x.id == storyId).isSeen = res.data.isSeen
    //   }
    // })

    this.selectedUserStorys = this.storysList.find((x: any) => x.userId == userId);
    let activeStory = 0
    const storyCount = this.selectedUserStorys.storyList.length

    const changeStory = () => {
      this.storyDetail = this.selectedUserStorys.storyList[activeStory];
      this.barWidth = 0
      animate();
    };

    clearInterval(this.timeout);
    const animate = () => {
      this.barWidth++;
      $("#story" + this.storyDetail.id).css({ 'width': `${this.barWidth}%` });
    };

    changeStory();
    this.openPopup();
    this.barWidth = 0;
    this.timeout = setInterval(() => {
      this.floorStoryTime = Math.floor(99 / storyCount) // Tam bölünmediği için yuvarladım
      if (this.barWidth === this.floorStoryTime) {
        activeStory += 1;
        if (activeStory < storyCount) {
          changeStory();
        } else {
          clearInterval(this.timeout);
          this.closePopup()
        }
      } else {
        animate();
      }
    }, this.storyDetail.storyDuration == 0 ? 80 : this.storyDetail.storyDuration);
  }

  file: any
  // imageSrc: any
  imageUrl: any
  videoUrl: any
  videoDuration: any
  fileType: any
  // GET FILE
  getFile(event: any) {
    this.file = <File>event.target.files[0];

    // this.imageSrc = URL.createObjectURL(event.target.files[0]);
    // $("#storyImage").attr("src", this.imageSrc);

    this.videoUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(event.target.files[0]));
    this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(event.target.files[0]));

    // VIDEO OR IMAGE
    this.fileType = this.file.type
  }

  // VIDEO DURATION
  getDuration(e: any) {
    this.videoDuration = e.target.duration;
  }

  floorDuration: any
  // UPLOAD STORY
  uploadStory() {
    if (this.videoDuration != null) {
      this.floorDuration = Math.floor(this.videoDuration * 10)
    }
    else {
      this.floorDuration = 0
    }

    let fileData = new FormData();
    fileData.append('userId', this.isLogin.id);
    fileData.append('images', this.file);
    fileData.append('fileDuration', this.floorDuration);

    this.services.uploadStory(fileData).subscribe((res: any) => {
      if (res.success) {

        this.services.getStorys(this.isLogin.id).then((res: any) => {
          if (res.success) {
            this.storysList = res.dataList
          }
        })

        this.closePopup();
      }
    })
  }

  // DELETE STORY
  deleteStory(storyId: any) {
    this.services.deleteStory(storyId).subscribe((res: any) => {
      if (res.success) {

        this.storysList.remove((x: any) => x.id == storyId)

        this.closePopup();
      }
    })
  }

}
