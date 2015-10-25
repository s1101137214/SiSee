/*
共通的JS部分，如搜尋按鈕、分類

*/

$(document).ready(function () {

    //景點自動搜尋
    $(".SearchButton").click(function () {
        window.location = "http://localhost:9542/Spots/Index/" + $(".SearchText").val() + "/";
    });
    
});

