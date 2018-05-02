using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace MVC_HW.Dao
{
    public class ShipperDao:BaseDao
    {

        public DataSet GetDropDowmDataSet()
        {

            SqlConnection conn = GetSqlConnection();
            String sql = "select * from [Sales].[Shippers]";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;


        }
    }
}