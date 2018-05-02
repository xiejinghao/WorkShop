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
        [DisplayName("客戶名稱")]
        public int CustomerID { get; set; }
        [DisplayName("負責員工")]
        public int EmployeeID { get; set; }
        [DisplayName("出貨公司名稱")]
        public int ShipperID { get; set; }

       
        public string CusCompanyName { get; set; }

      
        public string ShipCompanyName { get; set; }

       
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


        [DisplayName("運費")]
        public Decimal Freight { get; set; }
        [DisplayName("出貨地址")]
        public String ShipAddress { get; set; }
        [DisplayName("出貨城市")]
        public String ShipCity { get; set; }
        [DisplayName("出貨地區")]
        public String ShipRegion { get; set; }
        [DisplayName("郵遞區號")]
        public String ShipPostalCode { get; set; }
        [DisplayName("出貨國家")]
        public String ShipCountry { get; set; }


      
    }
}