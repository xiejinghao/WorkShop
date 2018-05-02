using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Dao;
using System.Data;
using System.Data.SqlClient;


namespace MVC_HW.Models.Service
{
    public class ShipperService
    {

        public List<Shippers> GetShipperList()
        {
            ShipperDao SD = new ShipperDao();
            DataSet ds = SD.GetDropDowmDataSet();
            List<Shippers> SList = ds.Tables[0].AsEnumerable().Select(m => new Shippers()
            {
              ShipperID=m.Field<int>("ShipperID"),
              CompanyName=m.Field<String>("CompanyName"),
              Phone=m.Field<String>("Phone")


            }).ToList();
            return SList;

        }
    }
}