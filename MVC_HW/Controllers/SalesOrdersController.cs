using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkShop;

namespace MVC_HW.Controllers
{
    public class SalesOrdersController : Controller
    {
        // GET: SalesOrders
        public ActionResult Index()
        {   
            WorkShop.Models.Order test= new WorkShop.Models.Order();
            var x=test.Initialize().Where(model=>model.OrderID==2);
            
            return View(x);
        }
        public ActionResult search()
        {
            

            return View();
        }
    }
}