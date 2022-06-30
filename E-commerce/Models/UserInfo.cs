using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce.Models
{
    public class UserInfo
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public string EmailId { get; set; }
        public int Zipcode { get; set; }
        public string country { get; set; }
        public string Password { get; set; }
    }
}
