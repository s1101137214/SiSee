/// <reference path="Share.js" />
//IIS http://localhost/SiSee_v1/

$(document).ready(function () {

    //檢查選取的景點位置取得對應的FB留言板
    CheckAreaAndFBcomment();

    //搜尋FB景點按鈕
    $(".SpotSearchButton").click(function () {
        SearchSpotName();
    });

    //檢查是否已收藏
    CheckFavoriteRecordIsSet();

    //載入google map
    loadScript();

    //檢查總評論數量
    CheckCommentStatus();

    //滑鼠經過時的變化，有問題先不使用

    //$(".CommentImg")
    //    .mouseover(function () {

    //        status = $(this).attr("src");

    //        if (statu = "/Content/img/star_b.png");

    //        $(this).attr("src", "/Content/img/star_s.png");
    //    })
    //   .mouseout(function () {
    //       $(this).attr("src", status);
    //   });

    $(".CommentImg").click(function () {
        if ($(this).attr('src') === "/Content/img/star_b.png") {

            var id = $(this).attr('id');

            $(".Grade").val(id);

            SetStarImg(id, "select")

        } else {

            var id = $(this).attr('id');

            console.log(id);

            $(".Grade").val("0");

            SetStarImg(id, "cancel")


        }
    });
    //SetCancelPlanButton()
    CheckPlanPoint();

});

function CheckPlanPoint() {

    var set = localStorage["PlanSet"];

    var spotName = $(".SpotName").val();

    if (set !== "True") {

        SetCancelPlanButton();

    } else {

        var length = JSON.parse(localStorage.getItem("SpotNamePoint")).length;

        var namePoint = JSON.parse(localStorage.getItem("SpotNamePoint"));

        var status = false;

        for (i = 0; i < length; i++) {
            if (namePoint[i] === spotName) {

                SetAddPlanButton();

                status = true;

                break;
            }
        };

        if (!status) {
            SetCancelPlanButton();
        }

    }
}

function SetCancelPlanButton() {
    $(".PlanButton").off('click');

    $(".PlanButton").html('加入路線規劃');
    $(".PlanButton").removeClass("btn-warning").addClass("btn-warning");

    $(".PlanButton").click(function () {
        GetPlanPoint();
    });
}

function SetAddPlanButton() {
    $(".PlanButton").off('click');

    $(".PlanButton").html('已加入規劃');
    $(".PlanButton").removeClass("btn-warning").addClass("btn-danger");

    $(".PlanButton").click(function () {
        if (confirm("確定取消嗎?")) {
            CancelPlanPiont();
        }
    });;

}

function CancelPlanPiont() {

    var length = JSON.parse(localStorage.getItem("SpotNamePoint")).length;

    var dataName = JSON.parse(localStorage.getItem("SpotNamePoint"));

    var spotName = $(".SpotName").val();

    var spotAdd = $(".SpotAddress").val();

    for (i = 0; i < length; i++) {

        if (dataName[i] === spotName) {

            var dataAdd = JSON.parse(localStorage.getItem("SpotAddPoint"));

            dataName[i] = "null";

            dataAdd[i] = "null";

            localStorage.setItem("SpotNamePoint", JSON.stringify(dataName));

            localStorage.setItem("SpotAddPoint", JSON.stringify(dataAdd));

            break;
        }
    }

    SetCancelPlanButton();
}

function GetPlanPoint() {

    var set = localStorage["PlanSet"];

    var spotName = $(".SpotName").val();

    var spotAdd = $(".SpotAddress").val();

    if (set !== "True") {

        localStorage.setItem("PlanSet", "True");

        var SpotNamePoint = new Array();

        SpotNamePoint[0] = spotName;

        console.log(spotName);

        localStorage.setItem("SpotNamePoint", JSON.stringify(SpotNamePoint));

        var SpotAddPoint = new Array();

        SpotAddPoint[0] = spotAdd;

        localStorage.setItem("SpotAddPoint", JSON.stringify(SpotAddPoint));

    } else {

        var dataName = JSON.parse(localStorage.getItem("SpotNamePoint"));

        var dataAdd = JSON.parse(localStorage.getItem("SpotAddPoint"));

        var length = JSON.parse(localStorage.getItem("SpotNamePoint")).length;

        dataName[length] = spotName;

        dataAdd[length] = spotAdd;

        localStorage.setItem("SpotNamePoint", JSON.stringify(dataName));

        localStorage.setItem("SpotAddPoint", JSON.stringify(dataAdd));

    }

    SetAddPlanButton();

}



//方法很鳥，有空再修改
function SetStarImg(id, status) {

    //被選取的圖示
    var srcS = "/Content/img/star_s.png";

    //當然就是還沒被選的圖示
    var srcB = "/Content/img/star_b.png";

    if (status == "select") {
        switch (id) {
            case "5":
                document.getElementById("5").setAttribute("src", srcS);
                document.getElementById("4").setAttribute("src", srcS);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "4":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcS);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "3":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "2":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "1":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcB);
                document.getElementById("1").setAttribute("src", srcS);
                break;
        }
    } else {
        switch (id) {
            case "5":
            case "4":
            case "3":
            case "2":
            case "1":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcB);
                document.getElementById("1").setAttribute("src", srcB);
                break;
        }
    }
}

function CheckAreaAndFBcomment() {

    var fbcomment = $(".fb-comments");

    fbcomment.hide();

    var areaName = $(".AreaName").val();

    switch (areaName) {
        case "北部":
            fbcomment.attr("data-href", "http://localhost/SiSee_v1/Spots/Detailsl/1");
            break;
        case "中部":
            fbcomment.attr("data-href", "http://localhost/SiSee_v1/Spots/Detailsl/2");
            break;
        case "南部":
            fbcomment.attr("data-href", "http://localhost/SiSee_v1/Spots/Detailsl/3");
            break;
        case "東部":
            fbcomment.attr("data-href", "http://localhost/SiSee_v1/Spots/Detailsl/4");
            break;
        default:
            fbcomment.attr("data-href", "http://localhost/SiSee_v1/");
            break;
    }

    fbcomment.show();

}

function SearchSpotName() {

    var spotName = $(".SpotName").val();

    var searchlink = "https://www.facebook.com/search/results/?q=" + spotName;

    window.open(searchlink);
}

function CheckFavoriteRecordIsSet() {
    $.ajax({
        url: url + "FavoriteRecords/CheckFavoriteRecord",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: $(".SpotID").val()
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        success: function (result) {
            if (result === "True") {
                $(".FavoriteRecordButton").off('click');

                $(".FavoriteRecordButton").html('已收藏');
                $(".FavoriteRecordButton").removeClass("btn-warning").addClass("btn-danger");

                $(".FavoriteRecordButton").click(function () {
                    if (confirm("確定取消收藏嗎?")) {
                        DeleteFavoriteRecord();
                    }
                });;
            } else {
                $(".FavoriteRecordButton").off('click');

                $(".FavoriteRecordButton").html('收藏');
                $(".FavoriteRecordButton").removeClass("btn-danger").addClass("btn-warning");

                $(".FavoriteRecordButton").click(function () {
                    CreateFavoriteRecord();
                });;
            }
        }
    });

    $.ajax({
        url: url + 'FavoriteRecords/CheckFavoriteRecordCount',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: $(".SpotID").val()
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        success: function (result) {
            if (result) {
                $(".FavoriteRecordCount").text(result);
            }
        }
    });
}

function CheckCommentStatus() {
    $.ajax({
        url: url + 'CommentRecords/CheckCommentRecordsCount',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: $(".SpotID").val()
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        success: function (result) {
            if (result) {
                $(".CommentCount").text(result);
            }
        }
    });
}

function CreateFavoriteRecord() {
    $.ajax({
        url: url + 'FavoriteRecords/CreateFavoriteRecord',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: $(".SpotID").val()
        }),
        type: 'POST',
        async: true,
        datatype: "text",
        processData: false,
        complete: function () {

        },
        beforeSend: function () {
            $.blockUI({
                message: "<h4><img src='http://localhost/SiSee_v1/Content/img/ajax-loader.gif'/> loading...</h4>",
                css: { backgroundColor: '#fff', color: 'black' }
            });
        },
        success: function (result) {
            console.log(result);
            if (result === 'False') {
                //錯誤訊息要改
                alert('請先登入才能進行收藏')
            } else {
                $(".FavoriteRecordButton").off('click');

                $(".FavoriteRecordButton").html('已收藏');
                $(".FavoriteRecordButton").removeClass("btn-warning").addClass("btn-danger");

                $(".FavoriteRecordButton").click(function () {
                    if (confirm("確定取消收藏嗎?")) {
                        DeleteFavoriteRecord();
                    }
                });;
            }
            $.unblockUI();
        },
        error: function (xhr, status) {
            $.unblockUI();
            alert(xhr);
        }
    })
}

function DeleteFavoriteRecord() {
    $.ajax({
        url: url + 'FavoriteRecords/DeleteFavoriteRecord',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            id: $(".SpotID").val()
        }),
        type: 'POST',
        async: true,
        processData: false,
        complete: function () {
            $(".FavoriteRecordButton").off('click');

            $(".FavoriteRecordButton").html('收藏');
            $(".FavoriteRecordButton").removeClass("btn-danger").addClass("btn-warning");

            $(".FavoriteRecordButton").click(function () {
                CreateFavoriteRecord();
            });;
        },
        error: function (xhr, status) {
            $.unblockUI();
            alert(xhr);
        }
    })
}

function initialize() {

    //初始化地圖時的定位經緯度設定
    var latlng = new google.maps.LatLng(23.973875, 120.982024); //台灣緯度Latitude、經度Longitude：23.973875,120.982024
    //初始化地圖options設定
    var mapOptions = {
        zoom: 15,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    //RWD設定
    google.maps.event.trigger(map, 'resize');

    var geocoder = new google.maps.Geocoder();
    GetLatlng($("#address").text(), "123", geocoder, map);
}

function loadScript() {

    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyC-FGBpt242zn5yJA__WVGX8k0V8EQp8x8&v=3.exp' +
        '&signed_in=true&callback=initialize';
    document.body.appendChild(script);

}

function GetLatlng(address, title, geocoder, resultsMap) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {

            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location,
                title: title
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}



