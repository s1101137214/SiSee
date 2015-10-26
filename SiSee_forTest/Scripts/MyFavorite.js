
$(document).ready(function () {

    $(".CancelFavoriteRecordButton").click(function () {
        if (confirm("確定取消追蹤嗎?")) {
            return true;
        }
        return false;
    });

});
