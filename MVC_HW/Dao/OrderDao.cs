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
            String sql = "select * from [Sales].Orders";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
             da.Fill(ds);
            return ds;




        }
    }
}