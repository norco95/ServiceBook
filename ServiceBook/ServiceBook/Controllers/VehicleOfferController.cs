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
    public class VehicleOfferController : Controller
    {
        // GET: VehicleOffer
        public ActionResult Index()
        {
            VehicleOfferViewModel VehicleOfferVM = new VehicleOfferViewModel();
         
            VehicleRepository vehicleRepository = new VehicleRepository();
            List<Vehicle> vehicles = vehicleRepository.getAllVehicle();
            VehicleOfferVM.VehicleOffers = new List<VehicleOffer>();
            foreach (var vehicle in vehicles)
            {
                var count=vehicle.Services.Count(x => x.Flag == 1);
                if(count>=2)
                {
                    VehicleOffer vehicleOffer = new VehicleOffer();
                    vehicleOffer.Vin = vehicle.VIN;
                    double sum = 0;
                    var i = 0;
                    var firstkm = 0;
                    var lastkm = 0;
                    foreach (var service in vehicle.Services)
                    {

                        if (service.Flag == 1)
                        { 
                            if (i == 0)
                            {
                                firstkm = service.CurrentKm;
                            }
                        if (i == vehicle.Services.Count() - 1)
                        {
                            lastkm = service.CurrentKm;
                        }
                        i++;
                        foreach (var intervention in service.SSI)
                        {
                            sum += intervention.ServiceIntervention.Price;
                        }
                    }
                    }
                    if (sum != 0)
                    {
                        vehicleOffer.AvgRepairCost = (sum / (lastkm - firstkm));
                    }
                    VehicleOfferVM.VehicleOffers.Add(vehicleOffer);
                }
            }
                


            

            return View(VehicleOfferVM);
        }
    }
}