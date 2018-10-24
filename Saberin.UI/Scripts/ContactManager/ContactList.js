ContactManager.ContactList = (() => {

    class ContactList {
        constructor(list = []) {
            this.Items = list;
        }
        get WidgetId() {
            return "contactmanager-contactlist";
        }
        Search(searchTerm) {
            let lowered = searchTerm.toLowerCase();
            let results = this.Items.filter(contact =>

                contact.ContactId.toString().indexOf(lowered) !== -1 ||
                contact.FirstName.toLowerCase().indexOf(lowered) !== -1 ||
                contact.LastName.toLowerCase().indexOf(lowered) !== -1 ||

                contact.Address.some(address =>
                    address.Street.toLowerCase().indexOf(lowered) !== -1 ||
                    address.City.toLowerCase().indexOf(lowered) !== -1 ||
                    address.State.toLowerCase().indexOf(lowered) !== -1 ||
                    address.PostalCode.toLowerCase().indexOf(lowered) !== -1
                )
            );

            return results;
        }
        FilterWidget(searchTerm) {
            let results = this.Search(searchTerm);
            for (let item of this.Items) {
                $(`.contactitem[data-id='${item.ContactId}']`).addClass("hidden");
            }
            for (let item of results) {
                $(`.contactitem[data-id='${item.ContactId}']`).removeClass("hidden");
            }
        }
        Init() {
            let lastClick = -1;
            let checking = false;
            $(`#${this.WidgetId}`).on("click",".contactitem",e => {
                checking = false;
                let id = e.target.dataset.id;
                let time = Date.now();
                let delta = time - lastClick;
                // double click
                if (delta <= 500) {
                    ContactManager.Modal.EditContact.Show({ contactId: id });
                    $(`#contact-${id}`).collapse('show');
                } else {
                    checking = true;
                    setTimeout(e => {
                        if (checking) {
                            $(`#contact-${id}`).collapse('toggle');
                        }
                    }, 200);
                }
                lastClick = time;
            });

            $(`#add-contact`).click(e => {
                ContactManager.Modal.NewContact.Show();
            });
            $("#contactmanager-search").click(e => {
                this.FilterWidget($("#srch-term").val());
            });
            $("#srch-term").keyup(e => {
                // enter
                if (e.which == 13) {
                    this.FilterWidget(e.target.value);
                }
            });
        }
        async Refresh() {
            let result = await $.ajax({
                url: "/ContactManager/ContactList",
                cache: false
            });
            $("#contactmanager-contactlist").html(result);
        }

    }

    return new ContactList();

})();