using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Models;
using System.Data;
using System.Data.SqlClient;

namespace MVC_HW.Dao

{
    public class OrderDao:BaseDao
    {
        public DataSet GetDataSet() {
            String ConnStr = GetString();
            SqlConnection conn = new SqlConnection(ConnStr);
            String sql = "select d.CompanyName as Cuscom,b.CompanyName as shipcom,* from [Sales].Orders a left join Sales.Shippers b on a.ShipperID=b.ShipperID left join HR.Employees c on a.EmployeeID=c.EmployeeID left join Sales.Customers d on a.CustomerID=d.CustomerID";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
             da.Fill(ds);
            return ds;




        }
    }
}