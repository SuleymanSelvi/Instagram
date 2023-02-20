$(document).ready(function () {
    GetUserFollowers();
});


function GetUserFollowers() {

    var id = $("#userId").val();

    $.ajax({
        url: "/Home/GetUserFollowers",
        type: "GET",
        data: { "id" : id },
        success: function (res) {
            var data = JSON.parse(res);
            var userFollowersHtml = "";

            $.each(data, function (index, value) {
                userFollowersHtml += "   <div class='slimScrollDiv'> ";
                userFollowersHtml += "     <div id='activity'> ";
                userFollowersHtml += "        <div class='media border-bottom-1 pt-3 pb-3'> ";
                userFollowersHtml += "             <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userFollowersHtml += "            <img class='mr-3 rounded-circle' style='border: 1px solid #000000' width='55' height='55' src='" + value.UserImageFile + "'>";
                userFollowersHtml += "            </a>";
                userFollowersHtml += "            <div class='media-body'> ";
                userFollowersHtml += "                 <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userFollowersHtml += "                    <h5>" + value.UserName + "</h5> ";
                userFollowersHtml += "                    <p class='mb-0'>" + value.About + "</p>";
                userFollowersHtml += "                 </a> ";
                userFollowersHtml += "           </div> ";
                userFollowersHtml += "      </div> ";
                userFollowersHtml += "   </div> ";
                userFollowersHtml += "</div> ";

            });

            $("#getUserFollowers").append(userFollowersHtml);
        }
    });
}