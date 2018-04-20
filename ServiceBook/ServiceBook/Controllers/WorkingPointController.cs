using ServiceBook.DAL;
using ServiceBook.DAL.Models;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ServiceBook.Controllers
{
    public class WorkingPointController : Controller
    {
        VehicleServiceCompanyRepository vehicleServiceCompanyRepository = new VehicleServiceCompanyRepository(); 
        // GET: WorkingPoint
        public ActionResult Index()
        {
            ServicePlaceViewModel servicePlaceVM = new ServicePlaceViewModel();
            List<WorkingPoint> workingPoints = new List<WorkingPoint>();
            workingPoints = vehicleServiceCompanyRepository.GetAllWorkingPoint();
            servicePlaceVM.WorkingPoints = new List<WorkingPointViewModel>();
            if (workingPoints!=null)
            {
                foreach(var workingPoint in workingPoints)
                {
                    WorkingPointViewModel workingPointViewModel = new WorkingPointViewModel();
                    workingPointViewModel.ID = workingPoint.ID;
                    workingPointViewModel.City = workingPoint.City;
                    workingPointViewModel.Nr = workingPoint.Nr;
                    workingPointViewModel.Street = workingPoint.Street;
                    workingPointViewModel.Country = workingPoint.Country;
                    workingPointViewModel.Reviews = new List<string>();
                    workingPointViewModel.CompanyName = workingPoint.VehicleServiceCompany.ServiceName;
                    double rate=0;
                    int db = 0;
                    if(workingPoint.Services!=null)
                    {
                        foreach(var service in workingPoint.Services)
                        {
                            if(service.Review!=null)
                            {
                                workingPointViewModel.Reviews.Add(service.Review.Description);
                                rate += service.Review.Rate;
                                db++;
                            }
                        }
                    }
                    if (db != 0)
                    {
                        rate /= db;
                    }
                    if(rate<2)
                    {
                        rate = 0;
                    }
                    if(rate>=2 && rate<4)
                    {
                        rate = 2;
                    }
                    if(rate>=4 && rate <6)
                    {
                        rate = 3;
                    }
                    if (rate >= 6 && rate < 8)
                    {
                        rate = 4;
                    }
                    if ( rate >= 8)
                    {
                        rate = 5;
                    }
                    workingPointViewModel.Rate = rate;
                    servicePlaceVM.WorkingPoints.Add(workingPointViewModel);
                }
            }
            return View(servicePlaceVM);
        }  
    }
}