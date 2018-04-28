using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Dao;
using System.Data;

namespace MVC_HW.Models.Service
{
    public class OrderService
    {
        public List<Order> GetList()
        {
            OrderDao OD = new OrderDao();
            DataSet ds = OD.GetDataSet();
         
           

            List<Order> LOrder = ds.Tables[0].AsEnumerable().Select(m => new Order()
            {
                OrderID = m.Field<int>("OrderID"),
                CustomID=m.Field<int>("CustomerID")
            }).OrderBy(x=>x.CustomID).ToList();
            return LOrder;







        }
    }
}