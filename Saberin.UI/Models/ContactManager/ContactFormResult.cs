using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saberin.UI.Models.ContactManager
{
    public class ContactFormResult
    {
        public enum Results
        {
            Success = 0,
            RepositoryError = 1,
            InvalidAddress = 2
        }
        public Results ResultCode { get; set; }
        public string Message { get; set; }
    }
}