
$(document).ready(function () {
    //景點自動搜尋
    $(".SearchButton").click(function () {
        window.location = "http://localhost:53796/Spots/Index/" + $(".SearchText").val() + "/";
    });
});