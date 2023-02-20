var page = 0;
var isPosting = false;

$(document).ready(function () {
    // infinite scroll
    $(window).bind('scroll', function () {
        if ($(window).scrollTop() >= $('.container-fluid').offset().top - 150 + $('.container-fluid').outerHeight() - window.innerHeight) {
            if (!isPosting)
                GetTweets();
        }
    });

    UploadTweet();
    GetTweets();
    LikeTweets();
    UploadCommentDiv();
    UploadComment();
});

function UploadTweet() {
    $('#UploadTweet').click(function () {
        var textTweetAbout = $("#textTweetAbout").val();

        var fileUpload = $("#files").get(0);
        var files = fileUpload.files;

        // Create  a FormData object
        var fileData = new FormData();
        fileData.append("tweetImage", files[0]);

        // Adding more keys/values here if need
        fileData.append('textTweetAbout', textTweetAbout);

        $.ajax({
            url: '/Home/UploadTweet',
            type: "POST",
            processData: false,
            contentType: false,
            data: fileData,
            success: function (result) {
                if (result < 0) {
                    location.href = "/Home/Login";
                }
                else if (result > 0) {
                    $("#textTweetAbout").val("");
                    $("#card-tweets").html("");
                    $("#files").val(null);
                    page = 0;
                    GetTweets();
                }
            },
            error: function (err) {
                alert("Tekrar Deneyiniz !");
            }
        });

    });
}

function GetTweets() {

    var tweetId = $(".like-tweet").attr("tweetId");

    $("#preloader").show();
    isPosting = true;
    $.ajax({
        url: "/Home/GetTweets/",
        data: {
            "page": page,
            "tweetId": tweetId
        },
        type: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            var tweetHtml = "";

            $.each(data, function (index, value) {
                var date = new Date(parseInt(value.CreatedDate.substr(6)));
                var day = date.getDate();
                var month = date.getMonth();
                month += 1;
                var year = date.getFullYear();

                tweetHtml += "<div class='card' style='margin-top:-20px' order='" + index + "' >";
                tweetHtml += "   <div class='card-body'>";
                tweetHtml += "      <div class='media media-reply'>";
                tweetHtml += "         <a href='/Home/UserProfile/?Id=" + value.ProfileUserId + "'>";
                tweetHtml += "           <img class='mr-3 circle-rounded' style='border: 1px solid #000000' width='70px' height='70px' src='" + value.UserImage + "'>";
                tweetHtml += "         </a>";
                tweetHtml += "         <div>";
                tweetHtml += "              <div class='d-sm-flex justify-content-between mb-2'";
                tweetHtml += "                <h4 class='mb-sm-0'>";
                tweetHtml += "                    <a href='/Home/UserProfile/?Id=" + value.ProfileUserId + "'>" + value.UserName + "</a> <small class='text-muted ml-2'> &thinsp; " +
                    (day + "." + month + "." + year) + "</small>";
                tweetHtml += "                </h4>";
                tweetHtml += "              </div>";
                tweetHtml += "              <p style='font-size:15px'>" + value.TweetDescription + "</p>";
                tweetHtml += "              <a href='/Home/TweetDetail/" + value.Id + "'>";
                tweetHtml += "                   <img class='click' id='imageZoom' src='" + value.ImageFile + "' style='border: 1px solid #000000;' width='100%' height='100%'>";
                tweetHtml += "              </a>";
                tweetHtml += "         <button style='margin-top:15px;' type='button' class='btn btn-outline-primary like-tweet' tweetId='" + value.Id + "'><i class='fa fa-heart'>" + " " + value.TweetLikeCount +"</i></button>";
                tweetHtml += "         <button style='margin-top:15px; margin-left:10px' type='button' class='btn btn-outline-success /*comment-tweet*/ upload-comment-div' tweetId='" + value.Id + "'><i class='fa fa-comment'>" + " " + value.TweetCommentCount +"</i> </button>";
                tweetHtml += "            <div id='hideComment' style='justify-content:center; align-items:center; display:flex; margin-top:10px; display:none'>";
           /*     tweetHtml += "                 <img class='mr-3 circle-rounded' style='border: 1px solid #000000' src='" + value.SessionUserImageFile + "' width='65' height='65'>";*/
                tweetHtml += "                 <textarea id='textComment' cols='3' rows='3' maxlength='250' placeholder='💬 Cevabını Yanıtla...' class='form-control'></textarea>";
                tweetHtml += "                 <input style='margin-left:10px' type='button' value='Yanıtla' class='btn btn-twitter upload-comment' />";
                tweetHtml += "           </div>";
                tweetHtml += "         </div>";
                tweetHtml += "      </div>";
                tweetHtml += "   </div>";
                tweetHtml += "</div>";
            });

            $("#card-tweets").append(tweetHtml);
            page++;
            $("#preloader").hide();
            isPosting = false;

            LikeTweets();
            UploadCommentDiv();
            UploadComment();
        }
    });
}

function UploadCommentDiv() {
    $(".upload-comment-div").unbind().click(function () {

        var isVisible = $("#hideComment").is(":visible");
        !isVisible ? $("#hideComment").show(500) : $("#hideComment").hide(500);

    });
}

function UploadComment() {
    $(".upload-comment").click(function () {

        var tweetId = $(".upload-comment-div").attr("tweetId");
        var textComment = $("#textComment").val();

        $.ajax({
            url: '/Home/UploadComment',
            type: 'POST',
            data: {
                "tweetId": tweetId,
                "textComment": textComment
            },
            success: function (res) {
                if (res == -2) {
                    location.href = "/Home/Login"
                }
                if (res == -1) {
                    alert("Alanları Doldurunuz !");
                }
                if (res == 1) {
                    alert("Başarılı");
                }
            }
        });
    });
}

function LikeTweets() {
    $(".like-tweet").unbind().click(function () {

        var tweetId = $(this).attr("tweetId");

        $.ajax({
            url: '/Home/TweetLike',
            type: 'POST',
            data: { 'tweetId': tweetId },
            success: function (res) {
                if (res == "-1")
                {
                    location.href = "/Home/Login";
                }
                else if (res == "1")
                {
                    $("button[tweetId=" + tweetId + "]").removeClass();
                    $("button[tweetId=" + tweetId + "]").addClass("btn btn-primary");
                }
                else if (res == "2")
                {
                    $("button[tweetId=" + tweetId + "]").removeClass();
                    $("button[tweetId=" + tweetId + "]").addClass("btn btn-outline-primary");
                }
            }
        });
    });
}