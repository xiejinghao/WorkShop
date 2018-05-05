using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC_HW.Models
{
    public class SearchArgs
    {
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }

        [DisplayName("客戶名稱")]
        public int CustomerID { get; set; }
        [DisplayName("負責員工")]
        public int EmployeeID { get; set; }
        [DisplayName("出貨公司")]
        public int ShippingID { get; set; }
        [DisplayName("訂單時間")]
        public DateTime OrderDate { get; set; }
        [DisplayName("要求時間")]
        public DateTime RequiredDate { get; set; }
        [DisplayName("出貨時間")]
        public DateTime ShippedDate { get; set; }



    }
}