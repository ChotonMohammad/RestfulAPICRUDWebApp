

/*
///////////////////////////////////////

        ADMIN SIGN IN SECTION START

///////////////////////////////////////

*/
//console.log(platform);

$("#sign_in_form_submit").on("submit", function (e) {
    e.preventDefault();
    //toastr.error("Oops! Something went wrong.Something went wrong.", "Err

    if ($('#sign_in_form_submit')[0].checkValidity() === false) {
        event.stopPropagation();
    }
    else {
        let userName = $("#head_admin_login_username").val();
        let password = $("#head_admin_login_password").val();
        let role = "admin";

        //console.log(json);

        $.ajax({
            url: "/login/" + userName + "/" + password + "/" + role,
            type: "POST",
            //data: JSON.stringify(json),
            dataType: "json",
            traditional: true,
            success: function (resp) {
                if (!resp.success) {
                    toastr.error(resp.msg);
                }
                else {
                    localStorage.setItem("token", resp.jwt)
                    location.href = "/Home/Employees";
                }
            },
            error: function (a, b, err) {
                toastr.error(resp.msg);

            },
            beforeSend: function () {
                $("#login_btn").empty();
                $("#login_btn").append('<i class="fa fa-spinner fa-spin" style="font-size:24px"></i>');

            },
            complete: function () {
                $("#login_btn").empty();
                $("#login_btn").append('Login');

            }
        });

    }


});

/*
///////////////////////////////////////

        ADMIN SIGN IN SECTION END

///////////////////////////////////////
*/