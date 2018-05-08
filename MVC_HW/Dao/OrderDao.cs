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
            String sql = @"Update [Sales].[Orders] set CustomerID=@CustomerID ,EmployeeID=@EmployeeID,OrderDate=@OrderDate,RequiredDate=@RequiredDate,ShippedDate=@ShippedDate,Freight=@Freight,ShipperID=@ShipperID,ShipAddress=@ShipAddress,ShipCity=@ShipCity,ShipRegion=@ShipRegion,ShipPostalCode=@ShipPostalCode,ShipCountry=@ShipCountry where OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID",order.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate",order.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", order.RequireDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate",order.ShippedDate));
            cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
            if (order.ShipRegion == null)
            {
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
            }
            else {
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
            }

            
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
            cmd.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }

        public DataSet GetSearchDataSet(SearchArgs searchargs) {


            SqlConnection conn = GetSqlConnection();
            String sql = "select d.CompanyName as Cuscom,b.CompanyName as shipcom,* from [Sales].Orders a left join Sales.Shippers b on a.ShipperID=b.ShipperID left join HR.Employees c on a.EmployeeID=c.EmployeeID left join Sales.Customers d on a.CustomerID=d.CustomerID where a.OrderID=@OrderID or a.CustomerID=@CustomerID or c.EmployeeID=@EmployeeID or a.ShipperID=@ShipperID and a.OrderDate=@OrderDate or a.RequiredDate=@RequiredDate or a.ShippedDate=@ShippedDate";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", searchargs.OrderID));
            cmd.Parameters.Add(new SqlParameter("@CustomerID", searchargs.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", searchargs.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", searchargs.ShippingID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", searchargs.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", searchargs.RequiredDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", searchargs.ShippedDate));
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public int OrderInsert(Order order) {
            SqlConnection conn = GetSqlConnection();
            String sql = @"insert into  [Sales].[Orders] ([CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[ShipperID],[Freight],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry]) values (@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipperID,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", order.RequireDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
            cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
            if (order.ShipRegion == null)
            {
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
            }
            
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
            cmd.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
            cmd.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
            conn.Open();
            int x = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return x;
        }
        public void OrderDelete(int id) {
            SqlConnection conn = GetSqlConnection();
            String sql = @"DELETE FROM [Sales].[OrderDetails] WHERE OrderID = @OrderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", id));
            conn.Open();
            cmd.ExecuteNonQuery();
             sql = @"DELETE FROM [Sales].[Orders] WHERE OrderID = @OrderID";
             cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", id));

            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
    }
        
}