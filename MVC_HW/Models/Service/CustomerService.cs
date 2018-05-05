using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MVC_HW.Dao;

namespace MVC_HW.Models.Service
{
    public class CustomerService
    {
        public List<Customers> GetCustomerList()
        {
            CustomerDao ED = new CustomerDao();
            DataSet ds = ED.GetDropDowmDataSet();
            List<Customers> CList = ds.Tables[0].AsEnumerable().Select(m => new Customers()
            {
                CustomerID=m.Field<int>("CustomerID"),
                Address=m.Field<String>("Address"),
                CompanyName=m.Field<String>("CompanyName"),
                ContactName=m.Field<String>("ContactName")



            }).ToList();
            return CList;
          

        }


    }
}