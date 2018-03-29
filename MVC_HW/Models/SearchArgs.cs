using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_HW.Models
{
    public class SearchArgs
    {   
        public int OrderID { get; set; }

        public String CompanyName { get; set; }
        public String EmployeeID { get; set; }
        public String ShippingID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }



    }
}