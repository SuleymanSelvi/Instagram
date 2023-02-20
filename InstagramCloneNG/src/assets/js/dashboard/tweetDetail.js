$(document).ready(function () {
    GetComments();
    UploadCommentDiv();
});

function UploadCommentDiv() {
    $(".upload-comment-div").unbind().click(function () {

        var isVisible = $("#commentDiv").is(":visible");
        !isVisible ? $("#commentDiv").show(500) : $("#commentDiv").hide(500);
    });
}

function GetComments() {

    var tweetId = $("#tweetId").val();

    $.ajax({
        url: "/Home/GetTweetCommentsById",
        data: { "tweetId": tweetId },
        type: 'GET',
        success: function (res) {
            var data = JSON.parse(res)
            var commentHtml = "";

            $.each(data, function (index, value) {
                commentHtml += " <div class='media media-reply'>";
                commentHtml += "     <img class='mr-3 circle-rounded' src='" + value.UserImage + "' width='50' height='50'>";
                commentHtml += "     <div class='media-body'>";
                commentHtml += "         <div class='d-sm-flex justify-content-between mb-2'>";
                commentHtml += "            <h5 class='mb-sm-0'" + value.UserName + "<small class='text-muted ml-3'>" + value.CreatedDate + "</small></h5>";
                commentHtml += "        </div>";
                commentHtml += "        <p>" + value.Comments + "</p>";
                commentHtml += "     </div>";
                commentHtml += "    </div>";
                commentHtml += " </div>";
            });

            $("#getTweetComments").append(commentHtml);
        }
    });
}