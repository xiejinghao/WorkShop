using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Dao;
using System.Data;
using MVC_HW.Models;

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
                CusCompanyName =m.Field<String>("Cuscom"),
                ShipCompanyName = m.Field<String>("Shipcom"),
                EmployeeName =m.Field<String>("LastName")+ m.Field<String>("FirstName"),
                OrderDate=m.Field<DateTime?>("OrderDate"),
                RequireDate=m.Field<DateTime?>("RequiredDate"),
                ShippedDate=m.Field<DateTime?>("ShippedDate"),
                Freight=m.Field<Decimal>("Freight"),
                ShipAddress=m.Field<String>("ShipAddress"),
                ShipCity=m.Field<String>("ShipCity"),
                ShipRegion=m.Field<String>("ShipRegion"),
                ShipPostalCode=m.Field<String> ("ShipPostalCode"),
                ShipCountry=m.Field<String>("ShipCountry")
            }).ToList();
            return LOrder;
        }

        public void setOrder(Order Order)
        {
            OrderDao OD = new OrderDao();
            OD.OrderUpdate(Order);            
            
            
        }

        public List<Order> Search(SearchArgs searchargs)
        {
            OrderDao OD = new OrderDao();
            DataSet ds = OD.GetDataSet();
            IEnumerable<Order> LOrder = ds.Tables[0].AsEnumerable().Select(m => new Order()
            {
                OrderID = m.Field<int>("OrderID"),
                CusCompanyName = m.Field<String>("Cuscom"),
                ShipCompanyName = m.Field<String>("Shipcom"),
                EmployeeName = m.Field<String>("LastName") + m.Field<String>("FirstName"),
                OrderDate = m.Field<DateTime?>("OrderDate"),
                RequireDate = m.Field<DateTime?>("RequiredDate"),
                ShippedDate = m.Field<DateTime?>("ShippedDate"),
                Freight = m.Field<Decimal>("Freight"),
                ShipAddress = m.Field<String>("ShipAddress"),
                ShipCity = m.Field<String>("ShipCity"),
                ShipRegion = m.Field<String>("ShipRegion"),
                ShipPostalCode = m.Field<String>("ShipPostalCode"),
                ShipCountry = m.Field<String>("ShipCountry")
            });



            if (searchargs.OrderID.HasValue)
            {
                LOrder = LOrder.Where(m => m.OrderID == searchargs.OrderID.Value);
            }

            // 客戶名稱 (like 查詢)
           /* if (!string.IsNullOrWhiteSpace(searchargs.CompanyName))
            {
                LOrder =
                    LOrder.Where(
                        m => customerService.GetCompanyName(m.CustomerID).Contains(searchargs.CompanyName)
                    );
            }*/

            // 員工編號
            if (searchargs.EmployeeID.HasValue)
            {
                LOrder = LOrder.Where(m => m.EmployeeID == searchargs.EmployeeID.Value);
            }

            // 出貨公司
            if (searchargs.ShipperID.HasValue)
            {
                LOrder = LOrder.Where(m => m.ShipperID == searchargs.ShipperID.Value);
            }

            // 訂購日期
            if (searchargs.OrderDate.HasValue)
            {
                LOrder = LOrder.Where(m => m.OrderDate == searchargs.OrderDate.Value);
            }

            // 需要日期
            if (searchargs.RequiredDate.HasValue)
            {
                LOrder = LOrder.Where(m =>m.RequireDate.Value == searchargs.RequiredDate.Value);
            }

            // 出貨日期
            if (searchargs.ShipedDate.HasValue)
            {
                LOrder = LOrder.Where(m => m.ShippedDate == searchargs.ShipedDate.Value);
            }







            return LOrder.ToList();
        }

        public int insert(Order order)
        {
            OrderDao OD = new OrderDao();
            int x =OD.OrderInsert(order);
            return x;

        }

        public void Delete(int id)
        {
            OrderDao OD = new OrderDao();
             OD.OrderDelete(id);
            

        }
    }
}