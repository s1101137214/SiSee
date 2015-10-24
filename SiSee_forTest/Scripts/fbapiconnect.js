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

    //景點自動搜尋
    $(".FBloginbutton").click(function () {

        CheckUserLoginStatus();

        //       checkLoginState()
    });

});

// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {

    if (response.status === 'connected') {

        ConnentedInf();

    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
        //document.getElementById('status').innerHTML = 'Please log ' +
        //  'into this app.';
    } else {
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
        //document.getElementById('status').innerHTML = 'Please log ' +
        //  'into Facebook.';
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
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me?fields=id,name,email', function (response) {

        console.log(response);

        $("#FBName").text(response.name);

        $("#FBMail").text(response.email);

        //   $("#FBBirth").text(response.user_birthday);

        $('#LoginDialog').modal('show');

        $(".CreateNewUserButton").click(function () {

            if ((typeof response.email == "undefined")) {
                response.email = "";
            }

            CreateNewUser(response.id, response.name, response.email)
        });

    });

}

function ConnentedInf() {
    FB.api('/me', function (response) {

        $(".LoginedInfo span").text('Successful login for: ' + response.name);

        $(".LoginedInfo").show();
    });
}

function CreateNewUser(id, name, email) {
    $.ajax({
        url: '/Users/CreateByFB',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: id,
            name: name,
            email: email
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        complete: function () {
            $.unblockUI();

        },
        beforeSend: function () {
            $.blockUI({
                message: "<h4><img src='http://localhost:9542/Content/img/ajax-loader.gif'/> loading...</h4>",
                css: { backgroundColor: '#fff', color: 'black' }
            });
        },
        success: function (result) {
            alert("finish");
            $.unblockUI();
        },
        error: function (xhr, status) {
            $.unblockUI();
            alert(xhr);
        }
    })
}
function CheckUserLoginStatus() {
    $.ajax({
        url: '/Users/CheckLogined',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        complete: function () {
            $.unblockUI();
        },
        beforeSend: function () {
            $.blockUI({
                message: "<h4><img src='http://localhost:9542/Content/img/ajax-loader.gif'/> loading...</h4>",
                css: { backgroundColor: '#fff', color: 'black' }
            });
        },
        success: function (result) {
            console.log(result);
            $.unblockUI();
        },
        error: function (xhr, status) {
            $.unblockUI();
            alert(xhr);
        }
    })
}

