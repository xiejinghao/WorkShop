using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_HW.Controllers
{
    public class SalesOrdersController : Controller
    {
        public static Models.Order OrderList;
        // GET: SalesOrders
        public ActionResult Index()
        {
          OrderList = new Models.Order();
          MVC_HW.Models.Order test= new MVC_HW.Models.Order();
             var x =test.Initialize().Where(model=>model.OrderID==2);
            
            return View(x);
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