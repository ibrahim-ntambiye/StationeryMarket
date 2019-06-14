﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StationeryService;
using Stationery;


namespace StationeryWebApp.Controllers
{
    

    public class CustomersController : Controller
    {
        IStationeryServices service;
        public CustomersController()
        {
            service = new StationeryServices();
        }
        // GET: Customers
        public ActionResult SearchForCustomer(int customerId)
        {
            Customer customerFound = service.GetCustomer(customerId);
            
            return View(customerFound);
        }

        

        // GET: Customers/Details/5
        public ActionResult CustomerOrderDetails(int id)
        {
            OrdersPerCustomerDto details = service.OrdersPerCustomer(id);
            return View(details);
        }

        // GET: Customers/UpdateCustomerDetails
        public ActionResult UpdateCustomerDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCustomerDetails(CustomerDto customerUpdates)
        {
          Customer customerInformationUpdates =  service.UpdateCustomerInformation(customerUpdates);
            if (customerInformationUpdates != null)
            {
               // return Content("Customer Information Successfully Updated");
                return View("NewCustomerDetails", customerInformationUpdates);
            }
            return View("UpdateCustomerDetails");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                service.AddCustomer(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Customers/Edit/5
        public ActionResult UpdateCustomerInformation(int id)
        {
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
