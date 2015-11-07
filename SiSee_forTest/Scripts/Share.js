/*
共通的JS部分，如搜尋按鈕、分類

*/

//IIS http://localhost/SiSee_v1/
var url = "/SiSee_v1/"

$(document).ready(function () {

    //景點自動搜尋
    $(".SearchButton").click(function () {

        if ($(".SearchText").val() != "") {
            window.location = "http://localhost/SiSee_v1//Spots/Index/" + $(".SearchText").val() + "/";
        }

    });

    //轉換分數成星星 
    if ($(".ScoreList").text() != "") {

        $('.ScoreList').each(function () {
            var score = $(this).text();

            $(this).html("");

            switch (score) {
                case "5":
                    $(this).append('<img src="http://localhost/SiSee_v1/Content/img/star_s.png" class="ListCommentImg"/>');
                case "4":
                    $(this).append('<img src="http://localhost/SiSee_v1/Content/img/star_s.png" class="ListCommentImg"  />');
                case "3":
                    $(this).append(' <img src="http://localhost/SiSee_v1/Content/img/star_s.png" class="ListCommentImg"  />');
                case "2":
                    $(this).append('<img src="http://localhost/SiSee_v1/Content/img/star_s.png" class="ListCommentImg"  />');
                case "1":
                    $(this).append('<img src="http://localhost/SiSee_v1/Content/img/star_s.png" class="ListCommentImg" />');
                    break;
                case "0":
                    $(this).append('<img src="http://localhost/SiSee_v1/Content/img/star_b.png" class="ListCommentImg" title="評分數不足~" />');
                    break;
            }
            
        });
    }

});

