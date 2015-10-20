$(document).ready(function () {

    CheckAreaAndFBcomment();

    //景點自動搜尋
    $(".SpotSearchButton").click(function () {
        SearchSpotName();
    });

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

