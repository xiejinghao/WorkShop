using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_HW.Models;


namespace MVC_HW.Controllers
{
    public class SalesOrdersController : Controller
    {
        public static List<Models.Order> OrderList;
        public static List<Models.Employees> EmployeesList;
        public static List<Models.Shippers> ShippersList;
        // GET: SalesOrders
        public ActionResult Index()
        {
            
            OrderList = new Models.Order().Initialize();

           
            
            return View(OrderList);
        }
        public ActionResult Search()
        {
            EmployeesList = new Models.Employees().Initialize();
            ShippersList = new Models.Shippers().Initialize();
            List<SelectListItem> selectlistitem=new List<SelectListItem>();
            foreach (var x in EmployeesList) {
                selectlistitem.Add(new SelectListItem()
                {
                    Text =x.FirstName+x.LastName,
                    Value=x.EmployeeID.ToString()
                });
            }
            SelectList SL = new SelectList(selectlistitem);
            ViewBag.SL = SL.Items;
            return View();
        }
        [HttpPost]
        public ActionResult searchResult(Models.SearchArgs SearchArgs)

        {

            Console.WriteLine(SearchArgs);
            return View();
        }
    }
}