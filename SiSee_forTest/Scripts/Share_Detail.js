$(document).ready(function () {

    CheckAreaAndFBcomment();

    //景點自動搜尋
    $(".SpotSearchButton").click(function () {
        SearchSpotName();
    });

    //載入google map
    loadScript();

});

//檢查選取的景點位置取得對應的FB留言板
function CheckAreaAndFBcomment() {

    var fbcomment = $(".fb-comments");

    fbcomment.hide();

    var areaName = $(".AreaName").val();

    switch (areaName) {
        case "北部":
            fbcomment.attr("data-href", "http://localhost:9542/Spots/Detailsl/1");
            break;
        case "中部":
            fbcomment.attr("data-href", "http://localhost:9542/Spots/Detailsl/2");
            break;
        case "南部":
            fbcomment.attr("data-href", "http://localhost:9542/Spots/Detailsl/3");
            break;
        case "東部":
            fbcomment.attr("data-href", "http://localhost:9542/Spots/Detailsl/4");
            break;
        default:
            fbcomment.attr("data-href", "http://localhost:9542/");
            break;
    }

    fbcomment.show();

}

//搜尋FB
function SearchSpotName() {

    var spotName = $(".SpotName").val();

    var searchlink = "https://www.facebook.com/search/results/?q=" + spotName;

    window.open(searchlink);
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

    //加入標記點
    //marker = new google.maps.Marker({
    //    draggable: true,
    //    position: latlng,
    //    title: "台灣 Taiwan",
    //    map: map
    //});

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

