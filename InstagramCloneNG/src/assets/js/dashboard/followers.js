$(document).ready(function () {
    GetFollowers();
});


function GetFollowers() {
    $.ajax({
        url: "/Home/GetMyFollowers",
        type: "GET",
        success: function (res) {
            var data = JSON.parse(res);
            var followersHtml = "";

            $.each(data, function (index, value) {
                followersHtml += "   <div class='slimScrollDiv'> ";
                followersHtml += "     <div id='activity'> ";
                followersHtml += "        <div class='media border-bottom-1 pt-3 pb-3'> ";
                followersHtml += "             <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                followersHtml += "            <img class='mr-3 rounded-circle' style='border: 1px solid #000000' width='55' height='55' src='" + value.UserImageFile + "'>";
                followersHtml += "         </a>";
                followersHtml += "            <div class='media-body'> ";
                followersHtml += "                 <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                followersHtml += "                  <h5>" + value.UserName + "</h5> ";
                followersHtml += "                   <p class='mb-0'>" + value.About + "</p>";
                followersHtml += "                  </a> ";
                followersHtml += "             </div> ";
                followersHtml += "      </div> ";
                followersHtml += "   </div> ";
                followersHtml += "</div> ";
            });

            $("#getFollowers").append(followersHtml);
        }
    });
}