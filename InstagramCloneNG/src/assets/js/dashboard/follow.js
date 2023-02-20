$(document).ready(function () {
    GetFollow();
});


function GetFollow() {
    $.ajax({
        url: "/Home/GetMyFollow",
        type: "GET",
        success: function (res) {
            var data = JSON.parse(res);
            var followHtml = "";

            $.each(data, function (index, value) {
                followHtml += "   <div class='slimScrollDiv'> ";
                followHtml += "     <div id='activity'> ";
                followHtml += "        <div class='media border-bottom-1 pt-3 pb-3'> ";
                followHtml += "             <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                followHtml += "            <img class='mr-3 rounded-circle' style='border: 1px solid #000000' width='55' height='55' src='" + value.UserImageFile + "'>";
                followHtml += "            </a>";
                followHtml += "            <div class='media-body'> ";
                followHtml += "                 <a href='/Home/UserProfile/?Id=" + value.Id + "'> ";
                followHtml += "                    <h5>" + value.UserName + "</h5> ";
                followHtml += "                    <p class='mb-0'>" + value.About + "</p>";
                followHtml += "                 </a> ";
                followHtml += "           </div> ";
                followHtml += "      </div> ";
                followHtml += "   </div> ";
                followHtml += "</div> ";

            });

            $("#getFollow").append(followHtml);
        }
    });
}