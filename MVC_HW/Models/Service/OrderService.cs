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
            DataSet ds = OD.GetSearchDataSet(searchargs);
            List<Order> LOrder = ds.Tables[0].AsEnumerable().Select(m => new Order()
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
            }).ToList();
            return LOrder;
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