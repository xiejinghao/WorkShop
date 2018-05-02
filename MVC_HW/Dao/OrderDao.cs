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
            
            SqlConnection conn = GetSqlConnection();
            String sql = "select d.CompanyName as Cuscom,b.CompanyName as shipcom,* from [Sales].Orders a left join Sales.Shippers b on a.ShipperID=b.ShipperID left join HR.Employees c on a.EmployeeID=c.EmployeeID left join Sales.Customers d on a.CustomerID=d.CustomerID";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
             da.Fill(ds);
            return ds;





        }
       public void OrderUpdate(Order order) {
            SqlConnection conn = GetSqlConnection();
            String sql = @"Update [Sales].[Orders] set CustomerID=@CustomerID ,EmployeeID=@EmployeeID,OrderDate=CAST('@OrderDate' AS DATETIME),RequiredDate=CAST('@RequiredDate' AS DATETIME),ShippedDate=CAST('@ShippedDate' AS DATETIME),Freight=@Freight,ShipperID=@ShipperID,ShipAddress='@ShipAddress',ShipCity='@ShipCity',ShipRegion='@ShipRegion',ShipPostalCode=@ShipPostalCode,ShipCountry='@ShipCountry' where OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID",order.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate",SqlDbType.DateTime).Value=order.OrderDate);
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", SqlDbType.DateTime).Value=order.RequireDate);
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", SqlDbType.DateTime).Value=order.ShippedDate);
            cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
            cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
            cmd.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();











          

        }
       
    }
        
}