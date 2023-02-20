const express = require("express");
const socket = require("socket.io");
const { Socket } = require("socket.io-client");
const MongoClient = require('mongodb').MongoClient;
// App setup
const PORT = 5000;
const app = express();

const server = app.listen(PORT, function () {
  console.log(`Listening on port ${PORT}`);
  console.log(`http://localhost:${PORT}`);
});

// Socket setup 
const io = socket(server, {
  cors: {
    origin: '*',
  }
});

// Fetch ile başka siteden dosya çekme izni
const cors = require("cors");
const corsOptions = {
  origin: '*',
  credentials: true,
  optionSuccessStatus: 200,
}
app.use(cors(corsOptions))

var db;

MongoClient.connect('mongodb://localhost', function (err, client) {
  if (err) throw err;
  db = client.db('instagram');
});

app.get('/GetMessages', (req, res) => {

  const parseFrom = JSON.parse(req.query.from);
  const parseTo = JSON.parse(req.query.to);

  db.collection('messages').find({
    $or:
      [{
        $and: [
          { "from": parseFrom },
          { "to": parseTo }
        ]
      },
      {
        $and: [
          { "from": parseTo },
          { "to": parseFrom }
        ]
      }]
  }, { projection: { _id: 0 } }).toArray((err, result) => {
    if (err) throw err;
    //client.close();
    res.send(result)
  });

})

var users = [];

io.on("connection", function (socket) {

  socket.on('disconnect', () => {
    users = users.filter(item => item.socketId !== socket.id)
    io.emit("online", users)
  });

  // Odaya alıyor
  socket.on('register', (data) => {
    socket.join("directMessage")
    socket.join(data)

    users.push(
      {
        socketId: socket.id,
        userId: data
      });
    setTimeout(() => { io.emit("online", users) }, 500)
  });

  // Chat üzerinden kişiye normal mesaj
  socket.on('sendMessage', (data) => {
    const date = new Date();

    let messages =
      [{
        from: data.from,
        to: data.to,
        message: data.message,
        messageType: data.messageType,
        createdDate: date.getHours() + ":" + date.getMinutes()
      }];

    db.collection('messages').insertMany(messages, (err, result) => {
      if (err) throw err;
      // console.log(result.insertedCount + ' kayıt eklendi.');
      //client.close();
    });

    socket.to(data.to).emit("sendMessage", data)
  });

  // Post şeklinde kişiye mesaj
  socket.on('sendPostDM', (data) => {
    const date = new Date();

    let message =
      [{
        from: data.from,
        to: data.to,
        postId: data.postId,
        message: data.messageFile,
        messageDescription: data.messageDescription,
        userName: data.userName,
        userImage: data.userImage,
        messageType: data.messageType,
        createdDate: date.getHours() + ":" + date.getMinutes(),
      }];

    db.collection('messages').insertMany(message, (err, result) => {
      if (err) throw err;
      //client.close();
    });
  });

  // IS WRITING ?
  socket.on('writing', (data) => {
    socket.to(data.to).emit("isWriting", data)
  });

});