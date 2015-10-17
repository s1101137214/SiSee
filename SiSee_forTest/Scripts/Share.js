
$(document).ready(function () {
    //景點自動搜尋
    $(".SearchButton").click(function () {
        window.location = "http://localhost:63955/Spots/Index/" + $(".SearchText").val() + "/";
    });
});