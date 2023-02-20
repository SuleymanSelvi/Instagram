const express = require('express');
const app = express();
const router = express.Router();
var redis = require('redis');

router.get('/GetUserList', function (req, res) {


  var client = redis.createClient();

  client.connect();
  client.on('connect', async function () {
    console.log('Redis client bağlandı');

    const value = await client.get('PostList');
    res.send(value);
  });

  client.on('error', function (err) {
    console.log('Redis Clientda bir hata var ' + err);
  });

});

app.use('/', router);
app.listen(1453, function () {
  console.log('Sunucu çalışıyor...');
});