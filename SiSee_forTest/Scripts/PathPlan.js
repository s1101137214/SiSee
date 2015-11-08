$(document).ready(function () {
    //有加入景點規劃才顯示地圖
    if (localStorage.getItem("PlanSet") === "True" && JSON.parse(localStorage.getItem("SpotAddPoint")).length > 1) {
        //載入google map
        loadScript();
    }else{
        $("#msg").text("目前沒有規劃的路線喔~快加入你喜歡的景點吧!")
        $("#mapsContain").hide();
        $("#pathContain").hide();
        $("#pathContext").css("width", "98%");
    }

});

//清除全部的localStorage 
function ClearlocalStorage() {

    localStorage.clear();

    alert("已經重新規劃了唷!");

    window.location.reload();

}

function initialize() {
    var directionsService = new google.maps.DirectionsService();
    var directionsDisplay = new google.maps.DirectionsRenderer();

    //初始化地圖時的定位經緯度設定
    var latlng = new google.maps.LatLng(23.973875, 120.982024); //台灣緯度Latitude、經度Longitude：23.973875,120.982024
    //初始化地圖options設定
    var mapOptions = {
        zoom: 15,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    directionsDisplay.setMap(map);
    directionsDisplay.setPanel(document.getElementById("directions_panel"));
    calcRoute(directionsService, directionsDisplay);
}

function loadScript() {

    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyC-FGBpt242zn5yJA__WVGX8k0V8EQp8x8&v=3.exp' +
        '&signed_in=true&callback=initialize';
    document.body.appendChild(script);

}

function calcRoute(directionsService, directionsDisplay) {
    //取值
    if (localStorage.getItem("PlanSet") === "True") {

        var namePoint = JSON.parse(localStorage.getItem("SpotNamePoint"));

        var addPoint = JSON.parse(localStorage.getItem("SpotAddPoint"));

    }

    var nameCount = namePoint.length;
    if (nameCount >= 2) {
        $("#pathPnt").append("<span><img  class='point' src='http://localhost:9542/Content/img/point02.png'  /></span>");
        $("#pathPnt").append("<span>" + namePoint[0] + "</span><br />　" + "  | <br />");
        for (i = 1; i < namePoint.length - 1; i++) {
            $("#pathPnt").append("<span ><img class='point'' src='http://localhost:9542/Content/img/point01.png'  /></span>");
            $("#pathPnt").append("<span>" + namePoint[i] + "</span><br />　" + " | <br />");
        }
        $("#pathPnt").append("<span ><img class='point' src='http://localhost:9542/Content/img/point02.png'  /></span>");
        $("#pathPnt").append("<span>" + namePoint[nameCount - 1] + "</span><br />");
    }

    //經過地點
    var waypts = [];
    var request = {};
    var start = addPoint[0];
    var addPointCount = addPoint.length;
    var end = addPoint[addPointCount-1];
    for (var i = 1; i < addPoint.length - 1; i++) {
        if (addPoint[i] != "null" || addPoint[i] != null) {
            waypts.push({
                location: addPoint[i],
                stopover: true
            });
        }
    }
    //判斷是否為多點規劃
    if (addPoint.length <= 2) {
        if (addPoint.length == 1) {
            end = "";
        }
        request = {
            origin: start,
            destination: end,
            travelMode: google.maps.TravelMode.DRIVING //汽車路線
        };
    } else {
        request = {
            origin: start,
            waypoints:waypts,
            destination: end,
            optimizeWaypoints: true,
            travelMode: google.maps.TravelMode.DRIVING
        };
    }
    
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });
}
