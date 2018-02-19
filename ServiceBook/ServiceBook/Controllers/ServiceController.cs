using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceBook.Models;
using ServiceBook.DAL;
using Microsoft.AspNet.Identity;
using ServiceBook.DAL.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Nexmo.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace ServiceBook.Controllers
{
    public class ServiceController : Controller
    {

        private VehicleServiceCompanyOwnerRepository VehicleServiceCompanyOwnerRepository = new VehicleServiceCompanyOwnerRepository();
        private VehicleServiceCompanyRepository VehicleServiceCompanyRepository = new VehicleServiceCompanyRepository();
        private ServiceInterventionRepository ServiceInterventionRepository = new ServiceInterventionRepository();
        private WorkingPointRepository WorkingPointRepository = new WorkingPointRepository();
        private EmployeeRepository EmployeeRepository = new EmployeeRepository();
        private ServiceRepository ServiceRepository = new ServiceRepository();
        private VehicleRepository VehicleRepository = new VehicleRepository();
        // GET: Service
       
       
        public ActionResult Index()
        {

            var uid = User.Identity.GetUserId();
            if (uid == null)
            {
                Response.Redirect("Account/Login", false);
            }
            else
            {
                ServiceViewModel serviceviewmodel = new ServiceViewModel()
                {
                    VehicleServiceCompanies = VehicleServiceCompanyOwnerRepository.GetVehicleCompanies(uid)
                };
                //var json=JsonConvert.SerializeObject(serviceviewmodel, Formatting.Indented,
                //            new JsonSerializerSettings
                //            {
                //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //            });
                //ServiceViewModel sw = new ServiceViewModel();
                //sw=JsonConvert.DeserializeObject<ServiceViewModel>(json);
                return View(serviceviewmodel);
            }
            return View();

        }

        [HttpPost]
        public ActionResult AddCompany(VehicleServiceCompany data)
        {
            
            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            
            CCO cco=VehicleServiceCompanyRepository.AddVehicleServiceCompany(uid,data);
            success = true;
            if(cco==null)
            {
                success = false;
                message = "*This company exist in your list";
                return Json(new { success = success, messages = message}, JsonRequestBehavior.DenyGet);

            }
            return Json(new { success = success, messages = message, VehicleServiceCompany = cco.VehicleServiceCompany }, JsonRequestBehavior.DenyGet);


        }

        [HttpPost]
        public ActionResult DeletCompany(VehicleServiceCompany data)
        {

            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            VehicleServiceCompanyOwnerRepository.DeletVehicleServiceCompany(uid, data);
            success = true;
  
            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult AddWorkingPoint(WorkingPoint data)
        {

            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            WorkingPoint WorkingPoint=WorkingPointRepository.AddWorkingPoint(data);
            success = true;
            if(WorkingPoint==null)
            {
                success = false;
                message = "*This working point already exist!";
                return Json(new { success = success, messages = message}, JsonRequestBehavior.DenyGet);
            }
            return Json(new { success = success, messages = message, WorkingPoint=WorkingPoint }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult SelectWorkingPoint(WorkingPoint data)
        {
            bool success = false;
            var message = "";
            Session["SelectedWorkingPointId"] = data.ID;
            
            success = true;
            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee data)
        {

            bool success = false;
            var message = "";
            Employee Employee = EmployeeRepository.AddEmployee(data);
            success = true;
            
            return Json(new { success = success, messages = message, Employee = Employee }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletEmployee(Employee data)
        {

            bool success = false;
            var message = "";
            EmployeeRepository.DeletEmployee(data);
            success = true;
           
            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee data)
        {

            bool success = false;
            var message = "";
            EmployeeRepository.EditEmployee(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult AddService(Vehicle data)
        {

            bool success = false;
            var message = "";
            SW SW= null;
            var uid = User.Identity.GetUserId();
            if ((int)Session["SelectedWorkingPointId"] != -1)
            {
                SW=ServiceRepository.AddService((int)Session["SelectedWorkingPointId"], data);

            }
        
            success = true;

            return Json(new { success = success, messages = message, SW=SW }, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        public ActionResult AddIntervention(ServiceIntervention data)
        {

            bool success = false;
            var message = "";
            ServiceIntervention Intervention=ServiceInterventionRepository.AddIntervention(data);
            success = true;

            return Json(new { success = success, messages = message,Intervention=Intervention}, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletService(Service data)
        {

            bool success = false;
            var message = "";
            ServiceRepository.DeletService(data);
            success = true;

            return Json(new { success = success, messages = message}, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult SaveRepairs(Service data)
        {
            bool success = false;
            var message = "";
            Service Service = ServiceRepository.SaveRepaire(data);
            success = true; 
            return Json(new { success = success, messages = message,Service=Service}, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletWorkingPoint(WorkingPoint data)
        {
            bool success = false;
            var message = "";
            WorkingPointRepository.DeletWorkingPoint(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EndedService(Service data)
        {
            bool success = false;
            var message = "";
            ServiceRepository.EndedService(data);
            success = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(data.Vehicle.VehicleOwner.Email);
            mailMessage.From = new MailAddress("info.servicebook@gmail.com");
            mailMessage.Subject = "Your vehicle with identifier: " + data.Vehicle.Identifier + " it is ready!";
            mailMessage.Body = "Hello " + data.Vehicle.VehicleOwner.FirstName + " " + data.Vehicle.VehicleOwner.LastName + "!\n\n" + " Your vehicle with identifier: " + data.Vehicle.Identifier + " it is ready! " + " The repairs costs: " + data.Price + " EURO";
            mailMessage.Body += "\n List of repairs: \n";
            if (data.SSI != null)
            {
                foreach (var intervention in data.SSI)
                {
                    mailMessage.Body += intervention.ServiceIntervention.Name;
                    mailMessage.Body += ", Cost: ";
                    mailMessage.Body += intervention.ServiceIntervention.Price;
                    mailMessage.Body += "\n";
                }
            }
            mailMessage.Body += "\n";
            mailMessage.Body += "\n Next service date: " + data.NextVisitDate.ToString();
            mailMessage.Body += "\n Next service km: " + data.NextVisitKm;



            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("info.servicebook@gmail.com", "allamvizsga");



            smtpClient.Send(mailMessage);

               ////   var results = SMS.Send(new SMS.SMSRequest
               ////   {
               ////      from = Nexmo.Api.Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
               ////      to = "+40" + data.Vehicle.VehicleOwner.PhoneNumber.ToString(),
               ////      text = mailMessage.Body
               ////});

           // Vehicles = Vehicles


            return Json(new { success = success, messages = message}, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        public ActionResult EditServiceCompany(VehicleServiceCompany data)
        {
            bool success = false;
            var message = "";

            var exist=VehicleServiceCompanyRepository.EditServiceCompany(data);
            success = true;
            if (exist==0)
            {
                success = false;
                message = "*This company name already exist!";
            }
          

            return Json(new { success = success, messages = message}, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditService(Service data)
        {
            bool success = false;
            var message = "";
            ServiceRepository.EditService(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditWorkingPoint(WorkingPoint data)
        {
            bool success = false;
            var message = "";
            WorkingPointRepository.EditWorkingPoint(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditIntervention(ServiceIntervention data)
        {
            bool success = false;
            var message = "";
            ServiceInterventionRepository.EditServiceIntervention(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletIntervention(ServiceIntervention data)
        {
            bool success = false;
            var message = "";
            ServiceInterventionRepository.DeletServiceIntervention(data);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetVehicleHistory(Vehicle data)
        {
            bool success = false;
            var message = "";
            var VehicleHistory = ServiceRepository.GetVehicleHistory(data);
            success = true;

            return Json(new { success = success, messages = message ,VehicleHistory=VehicleHistory}, JsonRequestBehavior.DenyGet);
        }
        

    }
}