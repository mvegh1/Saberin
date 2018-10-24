let ContactManager = (() => {

    class ContactManager {
        constructor() {
            this.Modal = {};
        }
        Init() {
            this.ContactList.Init();
            this.Modal.AbstractContact.Init();
            this.Alert.Init();
        }
    }

    return new ContactManager();

})();