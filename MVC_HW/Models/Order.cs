using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW.Models
{
    public class Order
    {
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }
        
        public string CompanyName { get; set; }
        
        public String EmployeeName { get; set; }

        [DisplayName("訂購日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime? OrderDate { get; set; }

        [DisplayName("需要日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime? RequireDate { get; set; }

        [DisplayName("出貨日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime? ShippedDate { get; set; }

        

        public Decimal Freight { get; set; }

        public String ShipAddress { get; set; }

        public String ShipCity { get; set; }

        public String ShipRegion { get; set; }

        public String ShipPostalCode { get; set; }

        public String ShipCountry { get; set; }


      
    }
}