﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_HW.Models
{
    public class OrderDetail
    {
        public int? OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public String Count { get; set; }
    }
}