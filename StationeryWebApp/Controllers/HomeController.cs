using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationery;
using StationeryService;

namespace StationeryWebApp.Controllers
{
    public class HomeController : Controller
    {
        IStationeryServices service;
        public HomeController()
        {
            service = new StationeryServices();
        }
        public ActionResult Index()
        {
            List<Marker> markersList = service.GetAllMarkers();

            return View(markersList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}