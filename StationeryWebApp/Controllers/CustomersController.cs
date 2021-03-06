using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StationeryService;
using Stationery;
using StationeryWebApp.Models;

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

        //[HttpPost]
        //public ActionResult UpdateCustomerDetails(CustomerDto customerUpdates)
        //{
        //  Customer customerInformationUpdates =  service.UpdateCustomerInformation(customerUpdates);
        //    if (customerInformationUpdates != null)
        //    {
        //       // return Content("Customer Information Successfully Updated");
        //        return View("NewCustomerDetails", customerInformationUpdates);
        //    }
        //    return View("UpdateCustomerDetails");
        //}

        // GET: /Account/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                var customer = service.Login(loginModel.Username, loginModel.Password);
                if (customer.Username != null)
                {
                    Session["Username"] = customer.Username;
                    Session["Admin"] = customer.IsAdmin;
                Session["CustomerId"] = customer.CustomerId;
                }
            
                return RedirectToAction("Index","Home");
            
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

                return RedirectToAction("Login");
            }
            catch
            {
                return View("Error");
            }
        }
        //Get
        public ActionResult UpdateCustomerInformation()
        {
            Customer customer = service.GetCustomer((int)Session["CustomerId"]);

            return View("UpdateCustomerInformation",customer);
        }

        [HttpPost]
        public ActionResult UpdateCustomerInformation(Customer customerUpdates)
        {
            Customer customerUpdated = service.UpdateCustomerInformation(customerUpdates);
            customerUpdated.Password = "*********";
            return View("CustomerInformationUpdated", customerUpdated);
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
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Basket"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
