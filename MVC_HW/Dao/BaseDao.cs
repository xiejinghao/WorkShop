using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_HW.Dao
{
    public class BaseDao
    {
        public SqlConnection GetSqlConnection()
        {
            return  new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

        }
    }
}