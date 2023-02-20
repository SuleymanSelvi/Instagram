var page = 0;
var isPosting = false;

$(document).ready(function () {
    // infinite scroll
    $(window).bind('scroll', function () {
        if ($(window).scrollTop() >= $('.container-fluid').offset().top - 150 + $('.container-fluid').outerHeight() - window.innerHeight) {
            if (!isPosting)
                GetUserTweets();
        }
    });

    GetUserTweets();
    FollowUser();
});

function GetUserTweets() {
    $("#preloader").show();
    isPosting = true;

    var userId = $("#getUserTweetsById").val();
   
    $.ajax({
        url: "/Home/GetUserTweetsById",
        data: {
            "userId": userId,
            "page": page
        },
        type: 'GET',
        success: function (res) {
            var data = JSON.parse(res)
            var tweetHtml = "";

            $.each(data, function (index, value) {
                var date = new Date(parseInt(value.CreatedDate.substr(6)));
                var day = date.getDate();
                var month = date.getMonth();
                month += 1;
                var year = date.getFullYear();

                tweetHtml += "<div class='card' style='margin-top:-20px' order='" + index + "'>";
                tweetHtml += "   <div class='card-body'>";
                tweetHtml += "     <div class='media media-reply'>";
                tweetHtml += "          <img class='mr-3 circle-rounded' src='" + value.UserImage + "' style='border: 1px solid #000000' width='40' height='40'>";
                tweetHtml += "          <div>";
                tweetHtml += "             <div class='d-sm-flex justify-content-between mb-2'>";
                tweetHtml += "                 <h5 class='mb-sm-0'>" + value.UserName + "<small class='text-muted ml-2'>| &thinsp;" + (day + "." + month + "." + year) + "</small></h5>";
                tweetHtml += "             </div>";
                tweetHtml += "             <p style='font-size: 15px'>" + value.TweetDescription + "</p>";
                tweetHtml += "             <img src='" + value.ImageFile + "' style='border: 1px solid #000000; ' width='100%' height='100%'>";
                tweetHtml += "          </div>";
                tweetHtml += "      </div>";
                tweetHtml += "    </div>";
                tweetHtml += "</div>";
            });

            $("#card-user-tweets").append(tweetHtml);
            page++;
            $("#preloader").hide();
            isPosting = false;
        }
    });
}

function FollowUser() {
    $("#follow").click(function () {

        var followId = $("#followId").val();

        $.ajax({
            url: "/Home/FollowUser",
            type: "POST",
            data: {  "followId": followId },
            success: function (res) {
                if (res == "-1") {
                    location.href = "/Home/Login";
                }
                else if (res == "1") {
                    $("#follow").val("Takip Ediliyor");
                    $("#follow").removeClass();
                    $("#follow").addClass("btn btn-twitter w-100");
                }
                else if (res == "2") {
                    $("#follow").val("Takip Et");
                    $("#follow").removeClass();
                    $("#follow").addClass("btn btn-tumblr w-100");
                }
            }
        });
    });
}
