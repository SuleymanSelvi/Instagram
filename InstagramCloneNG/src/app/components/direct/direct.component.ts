import { Component, HostListener, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/services/services.service';
import { io, Socket } from 'socket.io-client'
import * as $ from 'jquery';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-direct',
  templateUrl: './direct.component.html',
  styleUrls: ['./direct.component.scss']
})
export class DirectComponent implements OnInit {

  socket: Socket

  constructor(private services: ServicesService, private route: ActivatedRoute, private sanitizer: DomSanitizer) {
    this.socket = io("http://localhost:5000/")
  }

  session: any
  userList: any
  timeout: any = null;

  // MESSAGE
  messageList: Array<any> = [];
  selectedUser: any
  selectedUserMessageList: any

  // MESAJ ÇEKME
  fromId: any
  toId: any

  ngOnInit(): void {
    this.session = localStorage.getItem("loginSession");
    this.session = JSON.parse(this.session)
    this.getMyFollow();

    this.socket.connect()

    // ONLINE - OFFLINE
    this.socket.on("online", (data) => {

      this.userList.map((x: any) => {
        x.isOnline = false;
      })

      data.forEach((item: any) => {
        if (this.userList.find((x: any) => x.id == item.userId)) {
          this.userList.find((x: any) => x.id == item.userId).isOnline = true;
        }
      });

    })

    // GELEN MESAJ
    this.socket.emit("register", this.session.id)
    this.socket.on("sendMessage", (data) => {
      let messageToUser = this.messageList.length == 0 ? null :
        this.messageList.find((x: any) => (x.to == data.to && x.from == data.from) || (x.to == data.from && x.from == this.session.id))

      if (messageToUser != null) {
        messageToUser.userMessageList.push({ message: data.message, from: data.from })
      }
      else {
        const messageItem = {
          from: data.from,
          to: data.to,
          userMessageList: [{ message: data.message, from: data.from }]
        }

        this.messageList.push(messageItem)
      }

      // KARŞI KULLANICININ ATTIĞI SON MESAJ
      this.userList.find((x: any) => x.id == data.from).lastMessage = data.message

      // KULLANICI SEÇİLİ DEĞİLSE GELEN MESAJ SAYISINI ARTIR
      if (this.selectedUser == undefined) {
        this.userList.find((x: any) => x.id == data.from).messageCount += 1;
      }
      // BİR KULLANICI SEÇİLİ AMA SEÇİLEN KULLANICI BAŞKASI İSE YİNE MESAJ SAYISINI ARTIR
      else if (this.selectedUser != undefined && this.selectedUser.id != data.from) {
        this.userList.find((x: any) => x.id == data.from).messageCount += 1;
      }

      if (this.selectedUser)
        this.updateSelectedUserMessage(this.selectedUser.id)

    })

    // KARŞI KULLANICI YAZIYOR MU ?   
    this.socket.on("isWriting", (data) => {
      if (data.to == this.session.id) {
        this.userList.find((x: any) => x.id == data.from).isWriting = data.messageLength
      }
    })

    // OKUNDU BİLGİSİ
    this.socket.on("sendMessage", async (data) => {
      if (data.from == this.selectedUser.id) {
        this.userList.find((x: any) => x.id == data.from).isCheck = true

        //const xy = this.messageList.find((x: any) => x.from == this.selectedUser.id).from
        // this.userList.find((x: any) => x.id == xy).isCheck = true
      }
    })
  }

  // USER WRITING
  writing(message: any, selectedUserId: any) {
    this.socket.emit("writing", {
      messageLength: message.length,
      from: this.session.id,
      to: selectedUserId
    }
    )
  }

  file: any
  fileType: any
  chatFileUrl: any
  imageUrl: any
  // GET FILE
  getFile(event: any) {
    this.file = <File>event.target.files[0];
    this.fileType = this.file.type
    this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(event.target.files[0]));

    $("#chatMessageDisable").attr('disabled', 'disabled');
    $('#chatMessageDisable').on('click', function () {
      $(this).removeAttr('disabled');
    });
  }

  // SEND MESSAGE
  sendMessage(message: any) {
    let date = new Date();

    // MESAJ GÖNDERDİĞİMİZ KİŞİ İLE GEÇMİŞ SOHBETİMİZ
    let messageToUser = this.messageList.find((x: any) =>
      (x.to == this.selectedUser.id && x.from == this.session.id) || (x.to == this.session.id && x.from == this.selectedUser.id))

    // FILE MESSAGE
    if (this.file != null) {
      let fileData = new FormData();
      fileData.append('userId', this.session.id);
      fileData.append('images', this.file);

      this.services.uploadChatImage(fileData).subscribe((res: any) => {
        this.chatFileUrl = res

        this.socket.emit("sendMessage", {
          from: this.session.id,
          to: this.selectedUser.id,
          message: this.chatFileUrl,
          messageType: this.fileType
        })

        if (messageToUser != null) {
          messageToUser.userMessageList.push({ message: this.chatFileUrl, from: this.session.id, createdDate: date.getHours() + ":" + date.getMinutes() })
        }
        else {
          const messageItem = {
            from: this.session.id,
            to: this.selectedUser.id,
            userMessageList: [{ message: this.chatFileUrl, from: this.session.id }],
            createdDate: date.getHours + '' + date.getMinutes
          }
          this.messageList.push(messageItem)
        }

      })
    }
    else {
      this.socket.emit("sendMessage", {
        from: this.session.id,
        to: this.selectedUser.id,
        message: message,
        messageType: 'text'
      })

      if (messageToUser != null) {
        messageToUser.userMessageList.push({ message: message, from: this.session.id, createdDate: date.getHours() + ":" + date.getMinutes() })
      }
      else {
        const messageItem = {
          from: this.session.id,
          to: this.selectedUser.id,
          userMessageList: [{ message: message, from: this.session.id }],
          createdDate: date.getHours() + ":" + date.getMinutes()
        }
        this.messageList.push(messageItem)
      }

    }

    this.updateSelectedUserMessage(this.selectedUser.id)

    // MESAJ ATTIKTAN SONRA FİLE DOSYASINI NULL'A ÇEKİYOR VE MESAJ İNPUTUNU AÇIYOR
    this.file = null
    this.imageUrl = null
    $("#chatMessageDisable").removeAttr('disabled');

    // BENİM ATTIĞIM SON MESAJ
    this.userList.find((x: any) => x.id == this.selectedUser.id).lastMessage = message
  }

  // SELECTED USER
  selectUser(userId: any) {

    // BAŞKA KULLANICI SEÇİLİNCE FİLE DOSYASINI NULL'A ÇEKİYOR VE MESAJ İNPUTUNU AÇIYOR
    this.file = null
    $("#chatMessageDisable").removeAttr('disabled');

    // DATABASEDEN MESAJ ÇEKME
    // this.route.queryParams.subscribe(queryParams => {
    //   this.fromId = queryParams['from'];
    //   this.toId = queryParams['to'];
    // })
    fetch('http://localhost:5000/GetMessages?from=' + this.session.id + '&to=' + userId + '')
      .then(res => res.json())
      .then(response => {

        if (response.length) {
          let messageItem: any;
          for (let index = 0; index < response.length; index++) {
            if (index == 0) {
              messageItem = {
                from: response[index].from,
                to: response[index].to,
                userMessageList:
                  [{
                    // Normal chat üzerinden mesajlar için
                    message: response[index].message, from: response[index].from, createdDate: response[index].createdDate,
                    // Post olarak chat üzerinden mesaj yollamak için gerekli propertyler
                    messageType: response[index].messageType, userName: response[index].userName, userImage: response[index].userImage,
                    messageDescription: response[index].messageDescription, postId: response[index].postId
                  }]
              }
            }
            else {
              messageItem.userMessageList.push({
                // Normal chat üzerinden mesajlar için
                message: response[index].message, from: response[index].from, createdDate: response[index].createdDate,
                // Post olarak chat üzerinden mesaj yollamak için gerekli propertyler
                messageType: response[index].messageType, userName: response[index].userName, userImage: response[index].userImage,
                messageDescription: response[index].messageDescription, postId: response[index].postId
              })
            }
          }
          this.messageList.push(messageItem)

          this.selectedUser = this.userList.find((x: any) => x.id == userId)
          this.selectedUserMessageList = this.messageList.find((x: any) =>
            (x.from == userId && x.to == this.session.id) || (x.from == this.session.id && x.to == userId))

          // MESAJ SAYISI SIFIRLAMA
          this.userList.find((x: any) => x.id == userId).messageCount = 0;

          // SON MESAJ
          this.userList.find((x: any) => x.id == this.selectedUser.id).lastMessage = response.find((x: any) =>
            (x.from == this.session.id && x.to == userId) || (x.from == userId && x.to == this.session.id)).message
        }
      })
  }

  // UPDATE CHAT
  updateSelectedUserMessage(userId: any) {
    this.selectedUserMessageList = this.messageList.find((x: any) =>
      (x.from == userId && x.to == this.session.id) || (x.from == this.session.id && x.to == userId))

    // mesaj attıktan sonra mesaj length ni sıfırlıyor
    this.selectedUser.isWriting = 0
  }

  getMyFollow() {
    this.services.getUserFollowingForChat(this.session.id).then((res: any) => {
      if (res.success) {
        this.userList = res.dataList
      }
    });
  }

  searchUser(userName: any) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      this.services.searchUser(userName).then((res: any) => {
        if (res.success) {
          this.userList = res.dataList
        }
        else if (userName.length == 0) {
          this.userList = this.getMyFollow()
        }
      })
    }, 300);
  }

}
