using ServiceBook.DAL;
using ServiceBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ServiceBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel HomeVM = new HomeViewModel();
            VehicleRepository VehicleRepository = new VehicleRepository();
            WorkingPointRepository WorkingPointRepository = new WorkingPointRepository();
            VehicleServiceCompanyRepository ServiceCompanyRepository = new VehicleServiceCompanyRepository();
            ReviewRepository ReviewRepository = new ReviewRepository();
            HomeVM.Vehicles = VehicleRepository.GetVehicleCount();
            HomeVM.Workingpoints = WorkingPointRepository.GetWorkingPointCount();
            HomeVM.Services = ServiceCompanyRepository.GetServiceCompanyCount();
            HomeVM.Reviews = ReviewRepository.GetRevirewCount();

            return View(HomeVM);
        }

      
        public void SendEmail(String text,String name,String emailtitle)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("info.servicebook@gmail.com");
            mailMessage.From = new MailAddress("info.servicebook@gmail.com");
            mailMessage.Subject = "Help!";
            Email email = new Email();
            mailMessage.Body = "";
            mailMessage.Body += "I am :" + name+"\n";
            mailMessage.Body += "My Email: " + emailtitle + "\n\n";
            mailMessage.Body += text ;
            
            


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("info.servicebook@gmail.com", "allamvizsga");

            mailMessage.IsBodyHtml = true;


            smtpClient.Send(mailMessage);
        }
        [HttpPost]
        public ActionResult Index(String message , String email , String name)
        {

            HomeViewModel HomeVM = new HomeViewModel();
            VehicleRepository VehicleRepository = new VehicleRepository();
            WorkingPointRepository WorkingPointRepository = new WorkingPointRepository();
            VehicleServiceCompanyRepository ServiceCompanyRepository = new VehicleServiceCompanyRepository();
            ReviewRepository ReviewRepository = new ReviewRepository();
            HomeVM.Vehicles = VehicleRepository.GetVehicleCount();
            HomeVM.Workingpoints = WorkingPointRepository.GetWorkingPointCount();
            HomeVM.Services = ServiceCompanyRepository.GetServiceCompanyCount();
            HomeVM.Reviews = ReviewRepository.GetRevirewCount();

            SendEmail(message, name, email);

            return View(HomeVM);
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