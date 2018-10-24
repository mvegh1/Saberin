ContactManager.Alert = (() => {

    class Alert {
        Show(message, level) {
            this.Hide("");
            $(`.alert-${level}`).removeClass("alert-hide").addClass("alert-show").addClass("in");
            $(`.alert-${level} .message`).html(message)
        }
        Hide(level = "") {
            if (level == "") {
                $(".alert").removeClass("in").addClass("alert-hide").removeClass("alert-show");
                return;
            }
            $(`.alert-${level}`).removeClass("in").addClass("alert-hide").removeClass("alert-show");
        }
        Init() {
            $(".alert").on("click", ".close", e => {
                this.Hide(e.target.parentNode.dataset.level);
            });
        }
    }

    return new Alert();

})();