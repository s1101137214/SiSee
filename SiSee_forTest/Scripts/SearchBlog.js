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

    //   $("#SearchName").text(spotName);

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
        },

        error: function (e) {
            console.log(e);
        }
    });

    function GetData(data) {

        $("#ResultCount").text(data.total);

        $("#ResultPage").text(data.page);

        for (var i = 0; i < data.articles.length; i++) {
            $(".table").append
                ("<tr '><td><img alt='Cinque Terre' class='.img-rounded' src=" + data.articles[i].thumb + "/></td>" + "<td><h5><a href='" + data.articles[i].link + "'> " + data.articles[i].title + "</a></h5></td></tr>");
        }
    }
}