$(function() {
    var Popup = function() {
        this.title = "Popup title";
        this.body = "Popup question";
        this.confirmBtnText = "Yes";
        this.cancelBtnText = "No";
    }

    Popup.prototype.SetTitle = function(title) {
        this.title = title;
    }

    Popup.prototype.SetBody = function (body) {
        this.body = body;
    }

    Popup.prototype.SetConfirmBtnText = function (title) {
        this.title = title;
    }

    
})