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
            string sql = @"Update [Sales].[Orders] set CustomerID=@CustomerID ,EmployeeID=@EmployeeID,OrderDate=@OrderDate,RequiredDate=@RequiredDate,ShippedDate=@ShippedDate,Freight=@Freight,ShipperID=@ShipperID,ShipAddress=@ShipAddress,ShipCity=@ShipCity,ShipRegion=@ShipRegion,ShipPostalCode=@ShipPostalCode,ShipCountry=@ShipCountry where OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID",Convert.ToInt32(order.CustomerID)));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID",Convert.ToInt32( order.EmployeeID)));
            cmd.Parameters.Add(new SqlParameter("@OrderDate",order.OrderDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", order.RequireDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", Convert.ToInt32(order.ShipperID)));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
            if (order.ShipRegion != null)
            {

                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
            }
            else {

                cmd.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
            }
            
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
            cmd.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();











          

        }
       
    }
        
}