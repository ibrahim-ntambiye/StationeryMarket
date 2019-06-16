using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Stationery;
using StationeryService;

namespace StationeryWebApp.Controllers
{
    public class AdminController : Controller
    {
        private IStationeryServices service;

        public AdminController()
        {
                service = new StationeryServices();
        }
        // GET View method
        public ActionResult Index()
        {
            if ((bool?)Session["Admin"] == true && Session["Username"] != null)
            {
                return View();
            }
            return Content("You don't have admin permissions to access this function");
        }
        // GET  customer order details View method
        public ActionResult GetCustomerOrderDetails()
        {
            if ((bool?)Session["Admin"] == true && Session["Username"] != null)
            {
                return View("GetCustomerOrderDetails");
            }
            return Content("You don't have admin permissions to access this function");
        }

        [HttpPost]
        public ActionResult GetCustomerOrderDetails(int customerId)
        {
            OrdersPerCustomerDto informationResult = service.OrdersPerCustomer(customerId);
            return RedirectToAction("CustomerOrderDetails", informationResult );
        }
        [HttpPost]
        public ActionResult CustomerOrderDetails(OrdersPerCustomerDto informationResults)
        {
            if ((bool?)Session["Admin"] == true && Session["Username"] != null)
            {
                return View("CustomerOrderDetails", informationResults);
            }
            return Content("You don't have admin permissions to access this function");
        }

        [HttpPost]
        public ActionResult CustomerSearch(int customerId)
        {
            
                Customer customer = service.GetCustomer(customerId);
                if (customer != null)
                {
                    return View("CustomerSearch", customer);
                }


                return Content("Customer Doesn't exist in the database");
           
        }
        
        //public ActionResult SearchResults(Customer customer)
        //{
        //    return View("CustomerSearch", customer);
        //}
        // GET: AllCustomers
        public ActionResult GetAllCustomers()
        {
            if ((bool?)Session["Admin"] == true && Session["Username"] != null)
            {
                List<Customer> allCustomers = service.GetAllCustomers();

                return View("GetAllCustomers", allCustomers);
            }
            return Content("You don't have admin permissions to access this function");
        }
        //Get: Deletecustomer
        public ActionResult DeleteCustomer()
        {
            if ((bool?)Session["Admin"] == true && Session["Username"] != null)
            {
                return View();
            }
            return Content("You don't have admin permissions to access this function");
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            service.DeleteCustomer(customerId);
            return RedirectToAction("GetAllCustomers");
        }


        // GET: Admin/Details
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Admin/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Admin/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Admin/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Admin/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
