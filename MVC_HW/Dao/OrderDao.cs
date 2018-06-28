﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Models;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;


namespace MVC_HW.Dao

{
  
    public class OrderDao:BaseDao
    {
        public Order GetOrderById(int orderId)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = "select * from Sales.Orders where OrderID = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", orderId));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataRow row = ds.Tables[0].Rows[0];
                Order order =
                new Order
                {
                    OrderID = int.Parse(row["OrderID"].ToString()),
                    CustomerID = int.Parse(row["CustomerID"].ToString()),
                    EmployeeID = int.Parse(row["EmployeeID"].ToString()),

                    OrderDate = DateTime.Parse(row["OrderDate"].ToString()),
                    RequireDate = DateTime.Parse(row["RequiredDate"].ToString()),
                    ShippedDate = (!string.IsNullOrWhiteSpace(row["ShippedDate"].ToString())) ? new DateTime?(DateTime.Parse(row["ShippedDate"].ToString())) : null,
                    ShipperID = (!string.IsNullOrWhiteSpace(row["ShipperID"].ToString())) ? new int?(int.Parse(row["ShipperID"].ToString())) : null,
                    Freight = (!string.IsNullOrWhiteSpace(row["Freight"].ToString())) ? new decimal?(decimal.Parse(row["Freight"].ToString())) : null,
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipAddress = row["ShipAddress"].ToString()
                };




                string sqlDetail = "select * from Sales.OrderDetails where OrderID = @Id";
                SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                cmdDetail.Parameters.Add(new SqlParameter("@Id", orderId));

                SqlDataAdapter adapterDetail = new SqlDataAdapter(cmdDetail);

                DataSet dsDetail = new DataSet();
                adapterDetail.Fill(dsDetail);
                order.OrderDetail = new List<OrderDetail>();
                foreach (DataRow detailRow in dsDetail.Tables[0].Rows)
                {
                    order.OrderDetail.Add(new OrderDetail
                    {
                        OrderID = int.Parse(detailRow["OrderID"].ToString()),
                        ProductID = int.Parse(detailRow["ProductID"].ToString()),
                        UnitPrice = decimal.Parse(detailRow["UnitPrice"].ToString()),
                        Qty = int.Parse(detailRow["Qty"].ToString()),
                    });
                }

                return order;
            }
        }
        public int AddNewOrderReturnNewOrderId(Order newOrder)
        {
            SqlConnection conn = GetSqlConnection();
            string sql = @"
                INSERT INTO [Sales].[Orders]
                            ([CustomerID]
                            ,[EmployeeID]
                            ,[OrderDate]
                            ,[RequiredDate]
                            ,[ShippedDate]
                            ,[ShipperID]
                            ,[Freight]
                            ,[ShipName]
                            ,[ShipAddress]
                            ,[ShipCity]
                            ,[ShipRegion]
                            ,[ShipPostalCode]
                            ,[ShipCountry])
                        VALUES
                            (@CustomerID
                            ,@EmployeeID
                            ,@OrderDate
                            ,@RequiredDate
                            ,@ShippedDate
                            ,@ShipperID
                            ,@Freight
                            ,@ShipName
                            ,@ShipAddress
                            ,@ShipCity
                            ,@ShipRegion
                            ,@ShipPostalCode
                            ,@ShipCountry)
                    SELECT SCOPE_IDENTITY()
                ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID", newOrder.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", newOrder.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", newOrder.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", newOrder.RequireDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", newOrder.ShippedDate.HasValue ? newOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", newOrder.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@Freight", newOrder.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", newOrder.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", newOrder.ShipCity));
            cmd.Parameters.Add(new SqlParameter("@ShipRegion", newOrder.ShipRegion ?? ""));
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", newOrder.ShipPostalCode ?? ""));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", newOrder.ShipCountry));

            int orderId;

            conn.Open();
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                foreach (var detail in newOrder.OrderDetail)
                {
                    if (detail.ProductID == 0)
                        continue;
                    SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                    cmdDetail.Transaction = transaction;
                    cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                    cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                    cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                    cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                    cmdDetail.ExecuteNonQuery();
                }
                // 全部動作做完後執行Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                // 有出問題則將此交易內的所有更動的資料Rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }

            return orderId;
        }
        public DataSet GetDataSet() {
            
            SqlConnection conn = GetSqlConnection();
            String sql = "select d.CompanyName as Cuscom,b.CompanyName as shipcom,* from [Sales].Orders a left join Sales.Shippers b on a.ShipperID=b.ShipperID left join HR.Employees c on a.EmployeeID=c.EmployeeID left join Sales.Customers d on a.CustomerID=d.CustomerID";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
             da.Fill(ds);
            return ds;





        }
       public void OrderUpdate(Order oldOrder) {
            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection conn = GetSqlConnection();
                // 更新主檔並刪除明細
                string sql = @"
                    UPDATE [Sales].[Orders]
                    SET 
                        CustomerID = @CustomerID
                        ,EmployeeID = @EmployeeID
                        ,OrderDate = @OrderDate
                        ,RequiredDate = @RequiredDate
                        ,ShippedDate = @ShippedDate
                        ,ShipperID = @ShipperID
                        ,Freight = @Freight
                        ,ShipName = @ShipName
                        ,ShipAddress = @ShipAddress
                        ,ShipCity = @ShipCity
                        ,ShipRegion = @ShipRegion
                        ,ShipPostalCode = @ShipPostalCode
                        ,ShipCountry = @ShipCountry
                    WHERE OrderID = @OrderID
            
                    DELETE FROM Sales.OrderDetails WHERE OrderID = @OrderID
                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", oldOrder.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerID", oldOrder.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", oldOrder.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", oldOrder.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", oldOrder.RequireDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", oldOrder.ShippedDate.HasValue ? oldOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", oldOrder.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@Freight", oldOrder.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", oldOrder.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", oldOrder.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", oldOrder.ShipRegion ?? ""));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", DBNull.Value/*oldOrder.ShipPostalCode ?? ""*/));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", oldOrder.ShipCountry));

                conn.Open();
                cmd.ExecuteNonQuery();

                int orderId = Convert.ToInt32(oldOrder.OrderID);

                string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                foreach (var detail in oldOrder.OrderDetail)
                {
                    if (detail.ProductID == 0)
                        continue;
                    SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                    cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                    cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                    cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                    cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                    cmdDetail.ExecuteNonQuery();
                }

                scope.Complete();
                conn.Close();
            }


        }

       /* public DataSet GetSearchDataSet(SearchArgs searchargs) {


            SqlConnection conn = GetSqlConnection();
            String sql = "select d.CompanyName as Cuscom,b.CompanyName as shipcom,* from [Sales].Orders a left join Sales.Shippers b on a.ShipperID=b.ShipperID left join HR.Employees c on a.EmployeeID=c.EmployeeID left join Sales.Customers d on a.CustomerID=d.CustomerID where  a.CustomerID=@CustomerID and c.EmployeeID=@EmployeeID and a.ShipperID=@ShipperID and a.OrderDate=@OrderDate and a.RequiredDate=@RequiredDate and a.ShippedDate=@ShippedDate";
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            cmd.Parameters.Add(new SqlParameter("@CustomerID",searchargs.CustomerID));
            
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", searchargs.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", searchargs.ShippingID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", searchargs.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", searchargs.RequiredDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", searchargs.ShippedDate));
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }*/
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