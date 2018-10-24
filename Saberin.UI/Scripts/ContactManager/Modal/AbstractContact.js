ContactManager.Modal.AbstractContact = (() => {

    class AbstractContactModal extends ContactManager.Modal.Abstract {
        Cancel() {
            this.Hide();
        }
        static async AddAddressRow() {
            let html = await $.ajax({
                url: "/ContactManager/BlankAddressRow",
                cache: false,
                method: "GET"
            });
            $("#contact-addresses").append(html);
        }
        static Init() {
            $(`#${ContactManager.Modal.Abstract.WidgetId}`).on("click", ".fa-trash[data-target]", e => {
                let target = e.target.dataset.target;
                $(`#${target}`).remove();
            });

            $(`#${ContactManager.Modal.Abstract.WidgetId}`).on("click", "#add-address", e => {
                AbstractContactModal.AddAddressRow();
            });

            $(`#${ContactManager.Modal.Abstract.WidgetId}`).on("click", "#save-contact", e => {
                this.Save();
            });
        }
        async Show(data) {
            this.Footer = `<div>
                                <a id="modal-cancel" class="btn btn-white" data-dismiss="modal">Cancel</a>
                                <a id="save-contact" class="btn btn-primary">Save</a>
                          </div>`;
            return super.Show(data);
        }
        static async Save() {
            try {
                let contact = this.SerializeForm();
                let invalidReason = this.ValidateContact(contact);
                if (invalidReason != "") {
                    ContactManager.Alert.Show(invalidReason, "warning");
                    return;
                }
                let result = await $.ajax({
                    url: "/ContactManager/ContactForm",
                    cache: false,
                    data: JSON.stringify(contact),
                    contentType: "application/json; charset=utf-8",
                    method: "POST"
                });
                if (result.ResultCode == 0) {
                    ContactManager.Alert.Show("Contact saved!", "success");
                    ContactManager.ContactList.Refresh();
                    this.Hide();
                } else {
                    ContactManager.Alert.Show(result.Message, "danger");
                }
            }
            catch (e) {
                ContactManager.Alert.Show("There was a problem with your request...", "danger");
                    
            }

        }
        static ValidateContact(contact) {
            if (contact.FirstName.trim() == "" || contact.LastName.trim() == "") {
                return "Invalid Name";
            }
            if (contact.Address.length == 0) {
                return "No Address";
            }
            let valid = contact.Address.every(address =>
                address.Street.trim() != "" &&
                address.City.trim() != "" &&
                address.State.trim() != "" &&
                address.PostalCode.trim() != ""
            );
            if (!valid) {
                return "Invalid Address";
            }
            return "";
        }
        static SerializeForm() {
            let contact = {};
            contact.ContactId = $("#contact-contactid").val();
            contact.FirstName = $("#contact-firstname").val();
            contact.LastName = $("#contact-lastname").val();
            contact.Address = [];
            for (let row of $(".address-row")) {
                let $row = $(row);
                let addressGuid = $row.data("addressguid");
                let addressId = $(`#contact-addressid-${addressGuid}`).val();
                let street = $(`#contact-street-${addressGuid}`).val();
                let city = $(`#contact-city-${addressGuid}`).val();
                let state = $(`#contact-state-${addressGuid}`).val();
                let postalCode = $(`#contact-postalcode-${addressGuid}`).val();
                let address = {
                    ContactId: contact.ContactId,
                    AddressId: addressId,
                    Street: street,
                    City: city,
                    State: state,
                    PostalCode: postalCode
                };
                contact.Address.push(address);
            }
            return contact;
        }
    }

    return AbstractContactModal;

})();