$(document).ready(function () {

    //載入google map
    loadScript();


    //取值
    var namePoint = JSON.parse(localStorage.getItem("SpotNamePoint"));

    var addPoint = JSON.parse(localStorage.getItem("SpotAddPoint"));

    //同陣列取法,地址和名稱同陣列編號,沒值預設"null"
    console.log(namePoint);

    console.log(namePoint[0]);

    console.log(addPoint);
});

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
    //var long = map.getDirectionsInfo();
    //alert(long.distance['text']);

}

function loadScript() {

    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyC-FGBpt242zn5yJA__WVGX8k0V8EQp8x8&v=3.exp' +
        '&signed_in=true&callback=initialize';
    document.body.appendChild(script);

}

function calcRoute(directionsService, directionsDisplay) {
    var start = "彰化鹿港";
    var waypoints =[ {'location': '彰化合美'}, {'location': '鹿港龍山寺'}];
    var end = "彰化福興";
    //var start = document.getElementById('start').value;
    //var end = document.getElementById('end').value;

    //var arrPoint = waypoints.split(",");
  
    //    //經過地點
    //    var waypts = [];
    //    for (var i = 0; i < arrPoint.length; i++) {
    //            waypts.push({
    //                    location: arrPoint[i],
    //                    stopover: true
    //            });
    //    }

    var request = {
        origin: start,
        waypoints:waypoints,
        destination: end,
        optimizeWaypoints: true,
        travelMode: google.maps.TravelMode.DRIVING
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });
}
