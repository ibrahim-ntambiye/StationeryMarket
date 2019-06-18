using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StationeryService;
using Stationery;
using System.Web.Mvc;
using StationeryWebApp.Models;

namespace StationeryWebApp.Controllers
{
    public class MarkersController : Controller
    {
        IStationeryServices service;
        public MarkersController()
        {
            service = new StationeryServices();
        }

        // GET: Markers
        public ActionResult AddMarker()
        {
            if (Session["Username"] != null && (bool)Session["Admin"])
            {
                return View();
            }

            return Content("You don't have the right permissions to access this function.");
        }

        [HttpPost]
        public ActionResult AddMarker(Marker marker, HttpPostedFileBase markerImage)
        {
            if (markerImage != null)
            {
                markerImage.SaveAs(HttpContext.Server.MapPath("./MarkerImages/") + markerImage.FileName);
                marker.ImageFileName = markerImage.FileName;
            }

            Marker addedMarker = service.AddMarker(marker);


            return View("SingleMarkerDetails", addedMarker);

        }

        [HttpGet]
        public ActionResult GetAllMarkers()
        {
            if (Session["Username"] != null && (bool)Session["Admin"])
            {
                List<Marker> listOfMarkers = service.GetAllMarkers();

                return View("", listOfMarkers);
            }
            return Content("You don't have the right permissions to access this function.");
        }

        [HttpGet]
        public ActionResult ViewSingleMarkerDetails(int markerId)
        {
            var marker = service.ViewMarker(markerId);
            return View("SingleMarkerDetails", marker);
        }
        //[HttpGet]
        //public ActionResult DeleteMarkerFromDb()
        //{

        //    return View();
        //}

        [HttpGet]
        public ActionResult DeleteMarkerFromDb(int markerId)
        {
            if (Session["Username"] != null && (bool)Session["Admin"])
            {
                service.DeleteMarker(markerId);
                return RedirectToAction("GetAllMarkers");
            }
            return Content("You don't have the right permissions to access this function.");
        }
        // GET: Markers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markers/Create
        [HttpPost]
        public ActionResult AddToBasket(int markerId, int quantity)
        {
            if (Session["Username"] != null)
            {
                var marker = service.ViewMarker(markerId);

                if (Session["Basket"] == null)
                {

                    List<Basket> sessionBasket = new List<Basket>();

                    sessionBasket.Add(new Basket
                    {
                        ItemId = markerId,
                        Quantity = quantity,
                        Price = (marker.Price * quantity),
                        ItemName = marker.Brand

                    });
                    Session["Basket"] = sessionBasket;

                }
                else
                {
                    var basket = (List<Basket>)Session["Basket"];


                    if (basket.Any(c => c.ItemId == markerId))
                    {

                        var item = basket.Find(a => a.ItemId == markerId);
                        item.Quantity += quantity;

                    }

                    else
                    {
                        basket.Add(new Basket()
                        {
                            ItemId = markerId,
                            Quantity = quantity,
                            ItemName = marker.Brand,
                            Price = (marker.Price * quantity)

                        });
                    }
                    Session["Basket"] = null;
                    Session["Basket"] = basket;
                }

                return View("ViewBasket", Session["Basket"]);
            }
            return RedirectToAction("Login", "Customers");
        }
        [HttpGet]
        public ActionResult ViewBasket()
          
        {
            if (Session["Username"] != null)
            {
                return View("ViewBasket", Session["Basket"]);
            }
            return Content(" Stop trying to hack into my system and login. ");
        }

            //if (Session["Username"] != null)
            //{
            //var Basket = (Dictionary<int, int>)Session["Basket"];

            //var buffer = Basket.Select(c => c.Key == markerId);

            //if (buffer != null)

            //{
            //    int[] array1 = { quantity, Basket[markerId] };
            //    int updatedQuantity = array1.Sum();

            //    Basket.Remove(markerId);
            //    Basket.Add(markerId, updatedQuantity);
            //}

            //Basket.Add(markerId, quantity);

        
        //}

        //return RedirectToAction("Login", "Customers");
        ////}

    //}

    // GET: Markers/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: Markers/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
        try
        {
            // TODO: Add update logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: Markers/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: Markers/Delete/5
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