using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceBook.Controllers
{
    public class WorkingPointController : Controller
    {
        // GET: WorkingPoint
        public ActionResult Index()
        {
             return View();
        }
    }
}