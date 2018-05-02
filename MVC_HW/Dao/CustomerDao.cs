using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace MVC_HW.Dao
{
    public class CustomerDao:BaseDao
    {

        public DataSet GetDropDowmDataSet()
        {

            SqlConnection conn = GetSqlConnection();
            String sql = "select * from  [Sales].[Customers]";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;


        }
    }
}