using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce.Models
{
        public class ProductMaster
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Colour { get; set; }
            public string RAM_Size { get; set; }
            public string Hard_Drive_Size { get; set; }
            public string Processor_Type { get; set; }
            public string MRP { get; set; }
            public int Quantity { get; set; }
            public string EmailId { get; set; }
        //public string ProductPhoto { get; set; }
    }
}