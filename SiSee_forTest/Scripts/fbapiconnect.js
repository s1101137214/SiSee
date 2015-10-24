$(document).ready(function () {

    //設定appid connect
    window.fbAsyncInit = function () {
        FB.init({
            appId: '156432928035989',
            status: true,
            xfbml: true,
            version: 'v2.4'
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    $(".FBloginbutton").click(function () {

        checkLoginState();

    });

});

// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {

    if (response.status === 'connected') {

        ConnentedInf();

    } else if (response.status === 'not_authorized') {

    } else {

    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {

        FB.login(function (response) {
            if (response.authResponse) {
                FetchUserInfromation();
            } else {

            }
        }, { scope: 'public_profile,email' })
        statusChangeCallback(response);
    });
}

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function FetchUserInfromation() {
    FB.api('/me?fields=id,name,email', function (response) {

        CheckUserDBStatus(response);

    });

}

function ConnentedInf() {
    FB.api('/me', function (response) {

        $(".LoginedInfo span").text('歡迎回來唷～ ' + response.name);

        $(".LoginedInfo").show();

    });

}

function CreateNewUser(response) {
    $('#LoginDialog').modal('show');

    $("#FBName").text(response.name);

    $("#FBMail").text(response.email);

    //   $("#FBBirth").text(response.user_birthday);

    $(".CreateNewUserButton").click(function () {

        if ((typeof response.email == "undefined")) {
            response.email = "";
        }

        $.ajax({
            url: '/Users/CreateByFB',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                id: response.id,
                name: response.name,
                email: response.email
            }),
            type: 'POST',
            async: true,
            datatype: "text",
            processData: false,
            complete: function () {
                window.location.replace("http://localhost:9542/Spots/Index");
            },
            beforeSend: function () {
                $.blockUI({
                    message: "<h4><img src='http://localhost:9542/Content/img/ajax-loader.gif'/> loading...</h4>",
                    css: { backgroundColor: '#fff', color: 'black' }
                });
            },
            success: function (result) {
                $.unblockUI();
            },
            error: function (xhr, status) {
                $.unblockUI();
                alert(xhr);
            }
        })
    });
}

function CheckUserDBStatus(response) {

    var loaded = false;

    $.ajax({
        url: '/Users/CheckLogined',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: response.id
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        complete: function () {
            if (loaded == true) {
                window.location.replace("http://localhost:9542/Spots/Index");
            }
        },
        beforeSend: function () {
            $.blockUI({
                message: "<h4><img src='http://localhost:9542/Content/img/ajax-loader.gif'/> loading...</h4>",
                css: { backgroundColor: '#fff', color: 'black' }
            });
        },
        success: function (result) {

            console.log('登入狀態:'+result)

            if (result === "False") {
                CreateNewUser(response);
            } else {
                loaded = true;
                ConnentedInf();
            }
        },
        error: function (xhr, status) {
            $.unblockUI();
            alert(xhr);
        }
    })

}

