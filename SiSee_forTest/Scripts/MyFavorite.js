
$(document).ready(function () {

    $(".CancelFavoriteRecordButton").click(function () {
        if (confirm("確定取消收藏嗎?")) {
            return true;
        }
        return false;
    });

});
