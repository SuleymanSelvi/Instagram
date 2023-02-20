var page = 0;
var isPosting = false;

$(document).ready(function () {
    // infinite scroll
    $(window).bind('scroll', function () {
        if ($(window).scrollTop() >= $('.container-fluid').offset().top - 150 + $('.container-fluid').outerHeight() - window.innerHeight) {
            if (!isPosting) {
                GetMyTweets();
            }
        }
    });

    GetMyTweets();
    UptadeProfileDiv();
    UpdateProfile();
});
 

function GetMyTweets() {
    $("#preloader").show();
    isPosting = true;
    $.ajax({
        url: "/Home/GetMyTweetsById",
        type: 'GET',
        data: { "page": page },
        success: function (res) {
            var data = JSON.parse(res)
            if (data == "Login") {
                location.href = "/Home/Login";
            }
            var tweetHtml = "";
            $.each(data, function (index, value) {
                var date = new Date(parseInt(value.CreatedDate.substr(6)));
                var day = date.getDate();
                var month = date.getMonth();
                month += 1;
                var year = date.getFullYear();

                tweetHtml += "<div class='card' style='margin-top:-20px' order='" + index + "' tweetId='" + value.Id + "'>";
                tweetHtml += "   <div class='card-body'>";
                tweetHtml += "        <button style='float:right;' type='button' class='btn btn-outline-danger delete-tweet' tweetId='" + value.Id + "'><i class='fa fa-trash'></i> </button>";
                tweetHtml += "          <div class='media media-reply'>";
                tweetHtml += "          <img id='userProfileImageTweet' class='mr-3 circle-rounded' src='" + value.UserImage + "' style='border: 1px solid #000000' width='40' height='40'>";
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

            $("#card-my-tweets").append(tweetHtml);

            page++
            $("#preloader").hide();
            isPosting = false;
            DeleteTweet();
        }
    });
}

function UptadeProfileDiv() {
    $("#updateProfilDiv").click(function () {

        //var x = document.getElementById("hideUpdate");
        //x.style.display == 'none' ? $("#hideUpdate").show(500) : $("#hideUpdate").hide(500);

        //if (x.style.display == 'none') {
        //    $("#hideUpdate").show(500);
        //    $("#hideUpdate").toggle('slide');
        //}
        //else {
        //    $("#hideUpdate").hide(500);
        //}

        var isVisible = $("#hideUpdate").is(":visible");
        !isVisible ? $("#hideUpdate").show(500) : $("#hideUpdate").hide(500);
    });
}

function UpdateProfile() {
    $("#updateProfile").click(function () {

        var userName = $("#userName").val();
        var userPassword = $("#userPassword").val();
        var email = $("#email").val();
        var about = $("#about").val();

        var fileUpload = $("#files").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        fileData.append("userImage", files[0]);
        fileData.append('userName', userName);
        fileData.append('userPassword', userPassword);
        fileData.append('email', email);
        fileData.append('about', about);

        $.ajax({
            url: '/Home/UptadeProfile',
            type: "POST",
            processData: false,
            contentType: false,
            data: fileData,
            success: function (result) {
                if (result < "-1") {
                    location.href = "/Home/Login";
                }
                else if (result == "0") {
                    $("#updateError").html("Tüm Alanları Doldurunuz !");
                }
                else {
                    $("#userPassword").val("");
                    $("#hideUpdate").hide(500);
                    $("#userProfileImage").attr("src", result/*.tweetImageFile*/);
                    $("#userProfileImageTweet").attr("src", result/*.tweetImageFile*/);
                    //$("#updateUserName").val(result.userName);
                    //$("#updateAbout").val(result.about);
                }
            },
            error: function (err) {
                $("#updateError").html("Tüm Alanları Doldurunuz !");
            }
        });
    });
}

function DeleteTweet() {
    $(".delete-tweet").unbind().click(function () {
        var c = confirm("Silmek İstediğinizden Eminmisiniz ?")
        if (c)
        {
            var tweetId = $(this).attr("tweetId");
            $.ajax({
                url: '/Home/DeleteTweet',
                type: "POST",
                data: { "tweetId": tweetId },
                success: function (res) {
                    if (res == -1) {
                        location.href("/Home/Login");
                    }
                    else if (res == 0) {
                        alert("Bir Hata Oluştu");
                    }
                    else if (res > 0) {
                        page = 0;
                        $("div[tweetId=" + tweetId + "]").remove();
                    }
                }
            });
        }
    });
}