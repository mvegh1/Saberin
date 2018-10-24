ContactManager.Modal.NewContact = (() => {

    class NewContactModal extends ContactManager.Modal.AbstractContact {
        async Load(data) {
            return $.ajax({
                url: "/ContactManager/NewContact",
                cache: false,
                method: "GET"
            });
        }
        async Show(data) {
            this.Title = "New Contact";
            return super.Show(data);
        }
    }

    return new NewContactModal();

})();