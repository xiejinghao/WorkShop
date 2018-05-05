using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_HW.Models;
using MVC_HW.Dao;
using MVC_HW.Models.Service;
using System.Data;

namespace MVC_HW.Controllers
{
    public class SalesOrdersController : Controller
    {
        public static List<Models.Order> OrderList;
        public static List<Models.Employees> EmployeesList;
        public static List<Models.Shippers> ShippersList;
        public static List<Models.Customers> CustomersList;
        // GET: SalesOrders
        public ActionResult Index()
        {
            OrderService OrderService = new OrderService();
            OrderList = OrderService.GetList();
            return View(OrderList);
        }
        public ActionResult Search()
        {
            EmployeesList = new Models.Employees().Initialize();
            ShippersList = new Models.Shippers().Initialize();
            CustomersList = new Models.Customers().Initialize();
            List<SelectListItem> selectlistitem = new List<SelectListItem>();
            foreach (var x in EmployeesList) {
                selectlistitem.Add(new SelectListItem()
                {
                    Text = x.FirstName + x.LastName,
                    Value = x.EmployeeID.ToString()
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
            ViewBag.SL = SL.Items;
            return View();
        }
        [HttpPost]

        public ActionResult searchResult(Models.SearchArgs SearchArgs)

        { 
            //var test = SearchArgs.ShippingID.Equals(OrderList[0].ShipperID);
            //var ViewOrder = OrderList.Where(x => x.ShipperID.ToString().Equals(SearchArgs.ShippingID));
           /* IEnumerable<Order> ViewOrder = OrderList;
            if (ModelState.IsValid) {
                if (SearchArgs.ShippingID != null) {
                    ViewOrder = ViewOrder.Where(x => x.ShipperID.ToString().Contains(SearchArgs.ShippingID));
                }
                if (SearchArgs.OrderID != null)
                {
                    ViewOrder = ViewOrder.Where(x => x.OrderID.ToString().Contains(SearchArgs.OrderID.ToString()));
                }
                if (SearchArgs.OrderDate != null)
                {
                    ViewOrder = ViewOrder.Where(x => x.OrderDate.ToString().Contains(SearchArgs.OrderDate.ToString()));

                }
                if (SearchArgs.RequiredDate != null)
                {
                    ViewOrder = ViewOrder.Where(x => x.RequireDate.ToString().Contains(SearchArgs.RequiredDate.ToString()));
                }
                if (SearchArgs.ShippedDate != null)
                {
                    ViewOrder = ViewOrder.Where(x => x.ShippedDate.ToString().Contains(SearchArgs.ShippedDate.ToString()));

                }
              
               var search=CustomersList.SingleOrDefault(x => x.CompanyName.Equals(SearchArgs.CompanyName));

                if (SearchArgs.CompanyName != null)
                {
                    ViewOrder = ViewOrder.Where(x=>x.CustomID.Equals(search.CustomerID));

                }





            }

    */

            return View(OrderList);
        }
        public ActionResult Delete(int id) {
            var search = OrderList.Find(x => x.OrderID.Equals(id));
            return View(search);
        }
        [HttpPost]
        public ActionResult DeleteTrue(int id) {

            var itemToRemove = OrderList.SingleOrDefault(x => x.OrderID == id);
            if (itemToRemove != null)
                OrderList.Remove(itemToRemove);

            return RedirectToAction("Index", "SalesOrders");
        }

        public ActionResult Edit(int id) {
            CustomersList = new CustomerService().GetCustomerList();
            EmployeesList = new EmployeeService().GetEmployeeList();
            ShippersList = new ShipperService().GetShipperList();
            /*  List<SelectListItem> selectlistitem = new List<SelectListItem>();
              foreach (var x in EmployeesList)
              {
                  selectlistitem.Add(new SelectListItem()
                  {
                      Text = x.FirstName + x.LastName,
                      Value = x.EmployeeID.ToString()
                  });
              }*/

            ViewBag.EL = new SelectList(EmployeesList, "EmployeeID", "FullName");

            //ViewBag.EL = EmployeesList;
            ViewBag.SL = ShippersList;
            ViewBag.CL = CustomersList;


            var result = OrderList.Find(x => x.OrderID.Equals(id));
            return View(result);

        }
        [HttpPost]
        public ActionResult EditComfirm(Models.Order order) {
            /* var search = OrderList.FirstOrDefault(x => x.OrderID == order.OrderID);
             if (search != null) {
                 search.Freight = order.Freight;
                 search.OrderDate = order.OrderDate;
                 search.RequireDate = order.RequireDate;
                 search.ShipAddress = order.ShipAddress;
                 search.ShipCity = order.ShipCity;
                 search.ShipCountry = order.ShipCountry;
                 search.ShippedDate = order.ShippedDate;
                 search.ShipperID = order.ShipperID;
                 search.ShipPostalCode = order.ShipPostalCode;
                 search.ShipRegion = order.ShipRegion;


             }*/

            OrderService OS = new OrderService();
            OS.setOrder(order);
            return RedirectToAction("Index", "SalesOrders");


        }
    }
}