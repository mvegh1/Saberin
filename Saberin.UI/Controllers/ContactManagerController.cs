using Saberin.Data.Model;
using Saberin.Data.Repository.ContactManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Saberin.UI.Controllers
{
    public class ContactManagerController : Controller
    {
 
        public ActionResult Index()
        {
            var contacts = Repository.GetContacts();
            return View(contacts);
        }

        public ActionResult ContactList()
        {
            var contacts = Repository.GetContacts();
            return View("_ContactList", contacts);
        }
        [HttpGet]
        public ActionResult NewContact()
        {
            var contact = new Contact();
            contact.Address = new List<Address>();
            return View("_AddEditContact",contact);
        }

        [HttpPost]
        public ActionResult EditContact(int contactId)
        {
            var contact = Repository.GetContactById(contactId);
            return View("_AddEditContact", contact);
        }

        [HttpGet]
        public ActionResult BlankAddressRow()
        {
            return View("_AddressEditorRow", new Address());
        }

        [HttpPost]
        public JsonResult ContactForm(Contact contact)
        {
            try
            {
                var result = Repository.Save(contact);

                if(result.ResultCode == Data.Repository.ContactManager.Result.Save.ResultCodes.InvalidAddress)
                {
                    return Json(new Models.ContactManager.ContactFormResult { ResultCode = Models.ContactManager.ContactFormResult.Results.InvalidAddress, Message = result.Message });
                }

                return Json(new Models.ContactManager.ContactFormResult { ResultCode = Models.ContactManager.ContactFormResult.Results.Success });
            }
            catch(Exception e)
            {
                return Json(new Models.ContactManager.ContactFormResult { ResultCode = Models.ContactManager.ContactFormResult.Results.RepositoryError, Message = "There was an error saving the contact" });
            }
            
        }

    }
}