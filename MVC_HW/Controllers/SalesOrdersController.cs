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
            SelectList EL = new SelectList(selectlistitem);
            ViewBag.EL = EL.Items;
            List<SelectListItem> selectlistitemShipper = new List<SelectListItem>();

            foreach (var x in ShippersList)
            {
                selectlistitemShipper.Add(new SelectListItem()
                {
                    Text = x.CompanyName,
                    Value = x.ShipperID.ToString()
                });
            }
            SelectList SL = new SelectList(selectlistitemShipper);
            ViewBag.SL = SL.Items ;
            return View();
        }
        [HttpPost]
        public ActionResult searchResult(Models.SearchArgs SearchArgs)

        {
            //var test = SearchArgs.ShippingID.Equals(OrderList[0].ShipperID);
            //var ViewOrder = OrderList.Where(x => x.ShipperID.ToString().Equals(SearchArgs.ShippingID));
            IEnumerable<Order> ViewOrder=OrderList;
            if (ModelState.IsValid) {
               if (SearchArgs.ShippingID != null) {
                    ViewOrder = OrderList.Where(x => x.ShipperID.ToString().Equals(SearchArgs.ShippingID));
                }
                


            }



            return View(ViewOrder);
        }
    }
}