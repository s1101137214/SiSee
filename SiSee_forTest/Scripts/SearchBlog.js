$(document).ready(function () {

    SearchBlogSpotName();

});

function SearchBlogSpotName() {

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return results[1] || 0;
        }
    }

    var spotName = $.urlParam('SearchText');

    $("#SearchName").val(spotName);

    //https://developer.pixnet.pro/#!/doc/pixnetApi/oauthApi API相關變數

    var page = "1";

    var url = "https://emma.pixnet.cc/blog/articles/search?key=" + spotName + "&format=json&type=tag&per_page=20&page=" + page;

    $.ajax({
        url: url,
        //       data: $('#sentToBack').serialize(),
        type: "GET",
        dataType: 'json',

        success: function (data) {
            console.log(data);
            GetData(data);
            $.unblockUI();;
        },
        complete: function () {
            $.unblockUI();
        },
        beforeSend: function () {
            $.blockUI({
                message: "<h4><img src='http://localhost:9542/Content/img/ajax-loader.gif'/> 讀取中...</h4>",
                css: { backgroundColor: '#fff', color: 'black' }
            });
        },

        error: function (e) {
            console.log(e);
        }
    });
    
    //取得blog api回傳資料 取得內容可在調整
    function GetData(data) {

        if (data.total > 0) {

            $("#ResultCount").text(data.total);

            $("#ResultPage").text(data.page);

            for (var i = 0; i < data.articles.length; i++) {
                $(".table").append
                    ("<tr '><td><img alt='Cinque Terre' class='img-rounded blogimg' src=" + data.articles[i].thumb + "/></td>" + "<td><h5><a href='" + data.articles[i].link + "'> " + data.articles[i].title + "</a></h5></td></tr>");
            }
        }
     
    }
}