$(document).ready(function () {
    GetUserFollow();
});


function GetUserFollow() {

    var id = $("#userId").val();

    $.ajax({
        url: "/Home/GetUserFollow",
        type: "GET",
        data: { "id" : id},
        success: function (res) {
            var data = JSON.parse(res);
            var userFollowHtml = "";

            $.each(data, function (index, value) {
                userFollowHtml += "   <div class='slimScrollDiv'> ";
                userFollowHtml += "     <div id='activity'> ";
                userFollowHtml += "        <div class='media border-bottom-1 pt-3 pb-3'> ";
                userFollowHtml += "             <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userFollowHtml += "            <img class='mr-3 rounded-circle' style='border: 1px solid #000000' width='55' height='55' src='" + value.UserImageFile + "'>";
                userFollowHtml += "            </a>";
                userFollowHtml += "            <div class='media-body'> ";
                userFollowHtml += "                 <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userFollowHtml += "                    <h5>" + value.UserName + "</h5> ";
                userFollowHtml += "                    <p class='mb-0'>" + value.About + "</p>";
                userFollowHtml += "                 </a> ";
                userFollowHtml += "           </div> ";
                userFollowHtml += "      </div> ";
                userFollowHtml += "   </div> ";
                userFollowHtml += "</div> ";

            });

            $("#getUserFollow").append(userFollowHtml);
        }
    });
}