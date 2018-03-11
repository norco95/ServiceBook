using ServiceBook.DAL;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceBook.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String Review, int Rate, int ID)
        {
            ReviewRepository ReviewRepository = new ReviewRepository();
            var VehicleOwner=ReviewRepository.AddReview(ID, Review, Rate);
            VehicleOwnerViewModel VehicleOwnerVM = new VehicleOwnerViewModel();
            VehicleOwnerVM.FirstName = VehicleOwner.FirstName;
            VehicleOwnerVM.LastName = VehicleOwner.LastName;
 
            return View(VehicleOwnerVM);
        }
    }
}