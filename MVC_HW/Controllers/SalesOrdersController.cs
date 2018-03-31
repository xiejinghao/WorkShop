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
        // GET: SalesOrders
        public ActionResult Index()
        {
            
            OrderList = new Models.Order().Initialize();

          
            
            return View(OrderList);
        }
        public ActionResult search()
        {
            

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