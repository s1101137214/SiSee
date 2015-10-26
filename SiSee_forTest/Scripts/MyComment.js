
$(document).ready(function () {

    $(".CancelCommentRecordButton").click(function () {
        if (confirm("確定刪除評論嗎?")) {
            return true;
        }
        return false;
    });

});
