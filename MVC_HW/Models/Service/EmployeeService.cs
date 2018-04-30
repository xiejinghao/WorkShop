using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Dao;
using System.Data;
using System.Data.SqlClient;

namespace MVC_HW.Models.Service
{
    public class EmployeeService
    {

        public List<Employees> GetEmployeeList() {
            EmployeeDao OD = new EmployeeDao();
            DataSet ds = OD.GetDropDowmDataSet();
            List<Employees> EList = ds.Tables[0].AsEnumerable().Select(m => new Employees()
            {
               EmployeeID=m.Field<int>("EmployeeID"),
                FirstName=m.Field<String>("FirstName"),
                LastName=m.Field<String>("LastName"),
                FullName=m.Field<String>("FullName")
                

            }).ToList();
            return EList;

        }
    }
}