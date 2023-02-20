$(document).ready(function () {
    Registir();
});

function Registir() {
    $(".login-form__btn").click(function () {
        $("#RegistirError").html();
        var userName = $("[name='UserName']").val();
        var userPassword = $("[name='userPassword']").val();
        var email = $("[name='email']").val();
        var about = $("[name='about']").val();

        $.ajax({
            url: "/User/Registir",
            type: "POST",
            data: {
                "userName": userName,
                "userPassword": userPassword,
                "email": email,
                "about": about
            },
            success: function (res) {
                console.log(res)
                if (res.Result == true) {
                    location.href = "/Home/Login";
                }
                else {
                    $("#RegistirError").html(res.ErrorMessage);
                }
            }
        });
    });
}