$(function () {
    var removeBtns = $(".removeAction");

    for (var i = 0; i < removeBtns.size() ; i++) {
        removeBtns.click(function () {
            var href = $(this).attr("data-href");
            showConfirmWindow(href);
        });
    }

    $(".popupWindow .no").click(function () {
        hideConfirmWindow();
    });

    function showConfirmWindow(href) {
        $(".popupWindow .yes").attr("href", href);

        $(".materialPopup").css("display", "flex");
        $(".popupWindow").animate({
            opacity: "1"
        });
    }

    function hideConfirmWindow() {
        $(".popupWindow").animate({
            opacity: "0"
        });
        $(".materialPopup").css("display", "none");
        $(".popupWindow .yes").attr("href", "#");
    }
});