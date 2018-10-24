ContactManager.Modal.Abstract = (() => {

    class AbstractModal {
        static get WidgetId() {
            return "contactmanager-modal";
        }
        get WidgetId() {
            return AbstractModal.WidgetId;
        }
        get Title() {
            return $("#modal-title").text();
        }
        set Title(title) {
            $("#modal-title").text(title);
        }
        get Footer() {
            return $("modal-footer").html();
        }
        set Footer(footer) {
            $("#modal-footer").html(footer);
        }
        async Load(data) {
            // Load the modal body here
        }
        async Show(data) {
            let body = await this.Load(data);
            $("#modal-body").html(body);
            $(`#${this.WidgetId}`).modal({ show: true })
        }
        static Hide() {
            $(`#${this.WidgetId}`).modal("hide")
        }
    }

    return AbstractModal;

})();