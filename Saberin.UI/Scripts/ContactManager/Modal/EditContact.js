ContactManager.Modal.EditContact = (() => {

    class EditContactModal extends ContactManager.Modal.AbstractContact {
        async Load(data) {
            return $.ajax({
                url: "/ContactManager/EditContact",
                cache: false,
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                method: "POST"
            });
        }
        async Show(data) {
            this.Title = "Edit Contact";
            return super.Show(data);
        }
    }

    return new EditContactModal();

})();