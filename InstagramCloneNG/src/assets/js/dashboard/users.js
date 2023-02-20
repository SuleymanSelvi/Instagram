$(document).ready(function () {
    GetUsers();
});

function GetUsers() {
    $.ajax({
        url: "/Home/GetUsers/",
        data: GetUsers,
        type: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            var userHtml = "";

            $.each(data, function (index, value) {
                userHtml += "   <div class='slimScrollDiv'> ";
                userHtml += "     <div id='activity'> ";
                userHtml += "        <div class='media border-bottom-1 pt-3 pb-3'> ";
                userHtml += "             <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userHtml += "            <img class='mr-3 rounded-circle' style='border: 1px solid #000000' width='55' height='55' src='" + value.UserImageFile + "'>";
                userHtml += "         </a>";
                userHtml += "            <div class='media-body'> ";
                userHtml += "                 <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                userHtml += "                  <h5>" + value.UserName + "</h5> ";
                userHtml += "                   <p class='mb-0'>" + value.About + "</p>";
                userHtml += "                  </a> ";
                userHtml += "             </div> ";
                userHtml += "      </div> ";
                userHtml += "   </div> ";
                userHtml += "</div> ";
            });

            $("#getUsers").append(userHtml);
        }
    });
}