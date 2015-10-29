
$(document).ready(function () {
    var defaultscore = $("#comment_grade").val();

    SetStarImg(defaultscore, "select")

    $(".CommentImg").click(function () {
        if ($(this).attr('src') === "/Content/img/star_b.png") {

            var id = $(this).attr('id');

            $("#comment_grade").val(id);

            SetStarImg(id, "select")

        } else {

            var id = $(this).attr('id');

            console.log(id);

            $("#comment_grade").val("0");

            SetStarImg(id, "cancel")


        }
    });

});

f

function SetStarImg(id, status) {

    //被選取的圖示
    var srcS = "/Content/img/star_s.png";

    //當然就是還沒被選的圖示
    var srcB = "/Content/img/star_b.png";

    if (status == "select") {
        switch (id) {
            case "5":
                document.getElementById("5").setAttribute("src", srcS);
                document.getElementById("4").setAttribute("src", srcS);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "4":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcS);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "3":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcS);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "2":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcS);
                document.getElementById("1").setAttribute("src", srcS);
                break;
            case "1":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcB);
                document.getElementById("1").setAttribute("src", srcS);
                break;
        }
    } else {
        switch (id) {
            case "5":
            case "4":
            case "3":
            case "2":
            case "1":
                document.getElementById("5").setAttribute("src", srcB);
                document.getElementById("4").setAttribute("src", srcB);
                document.getElementById("3").setAttribute("src", srcB);
                document.getElementById("2").setAttribute("src", srcB);
                document.getElementById("1").setAttribute("src", srcB);
                break;
        }
    }
}