using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce.Models
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int ProuctID { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustID { get; set; }
        public string Price { get; set; }

    }
}