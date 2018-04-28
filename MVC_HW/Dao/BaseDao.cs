using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_HW.Dao
{
    public class BaseDao
    {
        public string GetString() {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        }
    }
}