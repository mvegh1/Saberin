using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saberin.Data.Repository.ContactManager.Result
{
    public class Sanitize
    {
        public enum ResultCodes
        {
            Success = 0,
            InvalidAddress = 1
        }
        public ResultCodes ResultCode { get; set; }
        public string Message { get; set; }
    }
}
