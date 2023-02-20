$(document).ready(function () {
    Login();
});

function Login() {
    $(".login-form__btn").click(function () {
        //$(this).val("Giriş Yapılır");
        var loginButton = $(this);
        loginButton.val("Giriş Yapılıyor...");
        $("#LoginError").html();

        var userName = $("[name='userName']").val();
        var userPassword = $("[name='userPassword']").val();

        $.ajax({
            url: "/User/Login",
            type: "POST",
            data: {
                "userName": userName,
                "userPassword": userPassword
            },
            success: function (res) {
                if (res == "True") {
                    location.href = "/Home/Index";
                }
                else {
                    loginButton.val("Giriş Yap");
                    $("#LoginError").html("Lütfen Bilgilerinizi Kontrol Ediniz !");
                }
            }
        });
    });
}