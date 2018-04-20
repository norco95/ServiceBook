using ServiceBook.DAL;
using ServiceBook.DAL.Models;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceBook.Controllers
{
    public class AroundKmController : Controller
    {
        // GET: AroundKm
        public ActionResult Index()
        {
            AroundKmViewModel aroundKmVM = new AroundKmViewModel();
            aroundKmVM.Exist = false;
            return View(aroundKmVM);
        }

        //POST: GetAroundKm

        [HttpPost]
        public ActionResult GetAroundKm(String VehicleVin)
        {
            bool success = false;
            var message = "";
            VehicleRepository vehicleR = new VehicleRepository();
            Vehicle vehicle = new Vehicle();
            vehicle=vehicleR.GetVehicleByVIN(VehicleVin);
            AroundKmViewModel aroundKmVM = new AroundKmViewModel();
            aroundKmVM.Exist = false;
            if(vehicle!=null)
            {
                if (vehicle.Services.Count >= 3)
                {
                    aroundKmVM.Exist = true;
                }
                var lastService = vehicle.Services.OrderByDescending(x =>x.ServiceDate).First();
                aroundKmVM.LastKm = lastService.CurrentKm;
                aroundKmVM.LastRepairDate = lastService.ServiceDate;
               
                if (aroundKmVM.Exist == true)
                {
                    var i = 0;
                    var days = 0;
                    var km = 0;
                    var previousDate = lastService.ServiceDate;
                    var previousKm = lastService.CurrentKm;
                    foreach (var service in vehicle.Services.Where(y=>y.Flag==1).OrderByDescending(x => x.ServiceDate))
                    {
                        if (i == 2)
                        {
                            days = days + (int)((previousDate - service.ServiceDate).TotalDays);
                            km = km + (previousKm -service.CurrentKm);
                            break;
                        }
                       
                        i++;
                    }
                    aroundKmVM.AroundKm = aroundKmVM.LastKm + (int)(DateTime.Now-lastService.ServiceDate).TotalDays * (km/days);
                }
                else
                {
                    aroundKmVM.Exist = true;
                    aroundKmVM.AroundKm = lastService.CurrentKm;
                }
               
              
            


            }

            return Json(new { success = success, messages = message,Around = aroundKmVM }, JsonRequestBehavior.DenyGet);
        }

    }
}