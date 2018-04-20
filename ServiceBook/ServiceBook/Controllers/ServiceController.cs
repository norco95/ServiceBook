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
using System.IO;

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
                var vehicleCompanies = VehicleServiceCompanyOwnerRepository.GetVehicleCompanies(uid);
                var vehicleServiceCompanies = new List<VehicleServiceCompanyViewModel>();
                foreach (var vehicleCompany in vehicleCompanies)
                {
                    VehicleServiceCompanyViewModel VehicleServiceCompanyVM = new VehicleServiceCompanyViewModel();
                    if (vehicleCompany.WorkingPoints != null)
                    {
                        VehicleServiceCompanyVM.WorkingPoints = new List<WorkingPointViewModel>();
                        foreach (var workingPoint in vehicleCompany.WorkingPoints)
                        {
                            if (workingPoint.Flag == 0)
                            {
                                WorkingPointViewModel WorkingPointVM = new WorkingPointViewModel();
                                WorkingPointVM.City = workingPoint.City;
                                WorkingPointVM.Country = workingPoint.Country;
                                WorkingPointVM.Flag = workingPoint.Flag;
                                WorkingPointVM.ID = workingPoint.ID;
                                WorkingPointVM.Nr = workingPoint.Nr;
                                WorkingPointVM.Street = workingPoint.Street;
                                WorkingPointVM.Services = new List<ServiceViewModel>();
                                WorkingPointVM.Employees = new List<EmployeeViewModel>();
                                WorkingPointVM.ServiceInterventions = new List<ServiceInterventionViewModel>();

                                if (workingPoint.Employees != null)
                                {
                                    foreach (var employee in workingPoint.Employees)
                                    {
                                        if (employee.Flag == 0)
                                        {
                                            EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                                            EmployeeVM.Email = employee.Email;
                                            EmployeeVM.FirstName = employee.FirstName;
                                            EmployeeVM.Flag = employee.Flag;
                                            EmployeeVM.ID = employee.ID;
                                            EmployeeVM.LastName = employee.LastName;
                                            EmployeeVM.PhoneNumber = employee.PhoneNumber;
                                            EmployeeVM.WPID = employee.WPID;
                                            WorkingPointVM.Employees.Add(EmployeeVM);
                                        }
                                    }

                                }
                                if (workingPoint.ServiceInterventions != null)
                                {
                                    foreach (var serviceIntervention in workingPoint.ServiceInterventions)
                                    {
                                        if (serviceIntervention.Flag == 0)
                                        {
                                            ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                                            ServiceInterventionVM.Flag = serviceIntervention.Flag;
                                            ServiceInterventionVM.Name = serviceIntervention.Name;
                                            ServiceInterventionVM.Price = serviceIntervention.Price;
                                            ServiceInterventionVM.ID = serviceIntervention.ID;
                                            ServiceInterventionVM.Currency = new CurrencyViewModel();
                                            ServiceInterventionVM.Currency.ID = serviceIntervention.ID;
                                            ServiceInterventionVM.Currency.Name = serviceIntervention.Currency.Name;
                                            WorkingPointVM.ServiceInterventions.Add(ServiceInterventionVM);
                                        }
                                    }
                                }
                                if (workingPoint.Services != null)
                                {
                                    foreach (var service in workingPoint.Services)
                                    {
                                        if (service.Flag == 0)
                                        {
                                            ServiceViewModel ServiceVM = new ServiceViewModel();
                                            ServiceVM.CurrentKm = service.CurrentKm;
                                            ServiceVM.Flag = service.Flag;
                                            ServiceVM.ID = service.ID;
                                            ServiceVM.NextVisitDate = service.NextVisitDate;
                                            ServiceVM.NextVisitKm = service.NextVisitKm;
                                            ServiceVM.Price = service.Price;
                                            ServiceVM.ServiceDate = service.ServiceDate;
                                            ServiceVM.Vehicle = new VehicleViewModel();
                                            ServiceVM.Vehicle.ID = service.Vehicle.ID;
                                            ServiceVM.Vehicle.Identifier = service.Vehicle.Identifier;
                                            ServiceVM.Vehicle.VehicleOwner = new VehicleOwnerViewModel();
                                            ServiceVM.Vehicle.VIN = service.Vehicle.VIN;
                                            ServiceVM.Vehicle.VehicleOwner.Email = service.Vehicle.VehicleOwner.Email;
                                            ServiceVM.Vehicle.VehicleOwner.FirstName = service.Vehicle.VehicleOwner.FirstName;
                                            ServiceVM.Vehicle.VehicleOwner.ID = service.Vehicle.VehicleOwner.ID;
                                            ServiceVM.Vehicle.VehicleOwner.LastName = service.Vehicle.VehicleOwner.LastName;
                                            ServiceVM.Vehicle.VehicleOwner.PhoneNumber = service.Vehicle.VehicleOwner.PhoneNumber;
                                            ServiceVM.ServiceInterventions = new List<ServiceInterventionViewModel>();
                                            ServiceVM.Employees = new List<EmployeeViewModel>();

                                            if (service.SSI != null)
                                            {
                                                foreach (var ssi in service.SSI)
                                                {
                                                    ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                                                    ServiceInterventionVM.Currency = new CurrencyViewModel();
                                                    ServiceInterventionVM.Currency.ID = ssi.ServiceIntervention.Currency.ID;
                                                    ServiceInterventionVM.Currency.Name = ssi.ServiceIntervention.Name;
                                                    ServiceInterventionVM.Flag = ssi.ServiceIntervention.Flag;
                                                    ServiceInterventionVM.ID = ssi.ServiceIntervention.ID;
                                                    ServiceInterventionVM.Name = ssi.ServiceIntervention.Name;
                                                    ServiceInterventionVM.Price = ssi.ServiceIntervention.Price;
                                                    ServiceVM.ServiceInterventions.Add(ServiceInterventionVM);
                                                }
                                            }
                                            if (service.SE != null)
                                            {
                                                foreach (var se in service.SE)
                                                {
                                                    EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                                                    EmployeeVM.Email = se.Employee.Email;
                                                    EmployeeVM.FirstName = se.Employee.FirstName;
                                                    EmployeeVM.Flag = se.Employee.Flag;
                                                    EmployeeVM.ID = se.Employee.ID;
                                                    EmployeeVM.LastName = se.Employee.PhoneNumber;
                                                    EmployeeVM.PhoneNumber = se.Employee.PhoneNumber;
                                                    ServiceVM.Employees.Add(EmployeeVM);
                                                }
                                            }
                                            WorkingPointVM.Services.Add(ServiceVM);
                                        }
                                    }
                                }
                                VehicleServiceCompanyVM.WorkingPoints.Add(WorkingPointVM);
                            }
                        }
                    }
                    VehicleServiceCompanyVM.ID = vehicleCompany.ID;
                    VehicleServiceCompanyVM.ServiceName = vehicleCompany.ServiceName;
                    VehicleServiceCompanyVM.Flag = vehicleCompany.Flag;
                    vehicleServiceCompanies.Add(VehicleServiceCompanyVM);
                }
                MainViewModel serviceviewmodel = new MainViewModel()
                {
                    VehicleServiceCompanies = vehicleServiceCompanies
                };

                return View(serviceviewmodel);
            }
            return View();

        }


        //COMPANY
        [HttpPost]
        public ActionResult AddCompany(VehicleServiceCompanyViewModel data)
        {

            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            var vehicleServiceCompany = new VehicleServiceCompany();
            vehicleServiceCompany.ServiceName = data.ServiceName;
            CCO cco = VehicleServiceCompanyRepository.AddVehicleServiceCompany(uid, vehicleServiceCompany);
            success = true;
            if (cco == null)
            {
                success = false;
                message = "*This company exist in your list";
                return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
            }
            data.Flag = cco.VehicleServiceCompany.Flag;
            data.ID = cco.VehicleServiceCompany.ID;
            if (cco.VehicleServiceCompany.WorkingPoints != null)
            {
                foreach (var workingPoint in cco.VehicleServiceCompany.WorkingPoints)
                {
                    WorkingPointViewModel WorkingPointVM = new WorkingPointViewModel();
                    WorkingPointVM.City = workingPoint.City;
                    WorkingPointVM.Country = workingPoint.Country;
                    WorkingPointVM.Flag = workingPoint.Flag;
                    WorkingPointVM.ID = workingPoint.ID;
                    WorkingPointVM.Nr = workingPoint.Nr;
                    WorkingPointVM.Street = workingPoint.Street;

                    if (workingPoint.Employees != null)
                    {
                        foreach (var employee in workingPoint.Employees)
                        {
                            EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                            EmployeeVM.Email = employee.Email;
                            EmployeeVM.FirstName = employee.FirstName;
                            EmployeeVM.Flag = employee.Flag;
                            EmployeeVM.ID = employee.ID;
                            EmployeeVM.LastName = employee.LastName;
                            EmployeeVM.PhoneNumber = employee.PhoneNumber;
                            WorkingPointVM.Employees.Add(EmployeeVM);
                        }

                    }
                    if (workingPoint.ServiceInterventions != null)
                    {
                        foreach (var serviceIntervention in workingPoint.ServiceInterventions)
                        {
                            ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                            ServiceInterventionVM.Flag = serviceIntervention.Flag;
                            ServiceInterventionVM.Name = serviceIntervention.Name;
                            ServiceInterventionVM.Price = serviceIntervention.Price;
                            ServiceInterventionVM.Currency = new CurrencyViewModel();
                            ServiceInterventionVM.Currency.ID = serviceIntervention.ID;
                            ServiceInterventionVM.Currency.Name = serviceIntervention.Currency.Name;
                            WorkingPointVM.ServiceInterventions.Add(ServiceInterventionVM);
                        }
                    }
                    if (workingPoint.Services != null)
                    {
                        foreach (var service in workingPoint.Services)
                        {
                            ServiceViewModel ServiceVM = new ServiceViewModel();
                            ServiceVM.CurrentKm = service.CurrentKm;
                            ServiceVM.Flag = service.Flag;
                            ServiceVM.ID = service.ID;
                            ServiceVM.NextVisitDate = service.NextVisitDate;
                            ServiceVM.NextVisitKm = service.NextVisitKm;
                            ServiceVM.Price = service.Price;
                            ServiceVM.ServiceDate = service.ServiceDate;
                            ServiceVM.Vehicle = new VehicleViewModel();
                            ServiceVM.Vehicle.ID = service.Vehicle.ID;
                            ServiceVM.Vehicle.Identifier = service.Vehicle.Identifier;
                            ServiceVM.Vehicle.VehicleOwner = new VehicleOwnerViewModel();
                            ServiceVM.Vehicle.VIN = service.Vehicle.VIN;
                            ServiceVM.Vehicle.VehicleOwner.Email = service.Vehicle.VehicleOwner.Email;
                            ServiceVM.Vehicle.VehicleOwner.FirstName = service.Vehicle.VehicleOwner.FirstName;
                            ServiceVM.Vehicle.VehicleOwner.ID = service.Vehicle.VehicleOwner.ID;
                            ServiceVM.Vehicle.VehicleOwner.LastName = service.Vehicle.VehicleOwner.LastName;
                            ServiceVM.ServiceInterventions = new List<ServiceInterventionViewModel>();

                            if (service.SSI != null)
                            {
                                foreach (var ssi in service.SSI)
                                {
                                    ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                                    ServiceInterventionVM.Currency = new CurrencyViewModel();
                                    ServiceInterventionVM.Currency.ID = ssi.ServiceIntervention.Currency.ID;
                                    ServiceInterventionVM.Currency.Name = ssi.ServiceIntervention.Name;
                                    ServiceInterventionVM.Flag = ssi.ServiceIntervention.Flag;
                                    ServiceInterventionVM.ID = ssi.ServiceIntervention.ID;
                                    ServiceInterventionVM.Name = ssi.ServiceIntervention.Name;
                                    ServiceInterventionVM.Price = ssi.ServiceIntervention.Price;
                                    ServiceVM.ServiceInterventions.Add(ServiceInterventionVM);
                                }
                            }
                            if (service.SE != null)
                            {
                                foreach (var se in service.SE)
                                {
                                    EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                                    EmployeeVM.Email = se.Employee.Email;
                                    EmployeeVM.FirstName = se.Employee.FirstName;
                                    EmployeeVM.Flag = se.Employee.Flag;
                                    EmployeeVM.ID = se.Employee.ID;
                                    EmployeeVM.LastName = se.Employee.PhoneNumber;
                                    EmployeeVM.PhoneNumber = se.Employee.PhoneNumber;
                                }
                            }
                            WorkingPointVM.Services.Add(ServiceVM);
                        }
                    }
                    data.WorkingPoints.Add(WorkingPointVM);
                }
            }

            return Json(new { success = success, messages = message, VehicleServiceCompany = data }, JsonRequestBehavior.DenyGet);


        }

        [HttpPost]
        public ActionResult DeletCompany(VehicleServiceCompanyViewModel data)
        {
            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            VehicleServiceCompany VehicleServiceCompany = new VehicleServiceCompany();
            VehicleServiceCompany.ID = data.ID;
            VehicleServiceCompany.ServiceName = data.ServiceName;
            VehicleServiceCompanyOwnerRepository.DeletVehicleServiceCompany(uid, VehicleServiceCompany);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditServiceCompany(VehicleServiceCompanyViewModel data)
        {
            bool success = false;
            var message = "";
            VehicleServiceCompany VehicleServiceCompany = new VehicleServiceCompany();
            VehicleServiceCompany.ServiceName = data.ServiceName;
            VehicleServiceCompany.ID = data.ID;
            var exist = VehicleServiceCompanyRepository.EditServiceCompany(VehicleServiceCompany);
            success = true;
            if (exist == 0)
            {
                success = false;
                message = "*This company name already exist!";
            }

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }


        //WORKINGPOINT
        [HttpPost]
        public ActionResult AddWorkingPoint(WorkingPointViewModel data)
        {
            bool success = false;
            var message = "";
            var uid = User.Identity.GetUserId();
            WorkingPoint wp = new WorkingPoint();
            wp.City = data.City;
            wp.Country = data.Country;
            wp.Street = data.Street;
            wp.Nr = data.Nr;
            wp.VSCID = data.VSCID;
            WorkingPoint WorkingPoint = WorkingPointRepository.AddWorkingPoint(wp);
            success = true;
            if (WorkingPoint == null)
            {
                success = false;
                message = "*This working point already exist!";
                return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
            }
            wp.ID = WorkingPoint.ID;
            if (wp.Employees != null)
            {
                data.Employees = new List<EmployeeViewModel>();
                foreach (var employee in wp.Employees)
                {
                    EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                    EmployeeVM.Email = employee.Email;
                    EmployeeVM.FirstName = employee.FirstName;
                    EmployeeVM.Flag = employee.Flag;
                    EmployeeVM.ID = employee.ID;
                    EmployeeVM.LastName = employee.LastName;
                    EmployeeVM.PhoneNumber = employee.PhoneNumber;
                    data.Employees.Add(EmployeeVM);
                }
            }
            if (wp.ServiceInterventions != null)
            {
                data.ServiceInterventions = new List<ServiceInterventionViewModel>();
                foreach (var serviceIntervention in wp.ServiceInterventions)
                {
                    ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                    ServiceInterventionVM.Flag = serviceIntervention.Flag;
                    ServiceInterventionVM.Name = serviceIntervention.Name;
                    ServiceInterventionVM.Price = serviceIntervention.Price;
                    ServiceInterventionVM.Currency = new CurrencyViewModel();
                    ServiceInterventionVM.Currency.ID = serviceIntervention.ID;
                    ServiceInterventionVM.Currency.Name = serviceIntervention.Currency.Name;
                    data.ServiceInterventions.Add(ServiceInterventionVM);
                }
            }
            if (wp.Services != null)
            {
                data.Services = new List<ServiceViewModel>();
                foreach (var service in wp.Services)
                {
                    ServiceViewModel ServiceVM = new ServiceViewModel();
                    ServiceVM.CurrentKm = service.CurrentKm;
                    ServiceVM.Flag = service.Flag;
                    ServiceVM.ID = service.ID;
                    ServiceVM.NextVisitDate = service.NextVisitDate;
                    ServiceVM.NextVisitKm = service.NextVisitKm;
                    ServiceVM.Price = service.Price;
                    ServiceVM.ServiceDate = service.ServiceDate;
                    ServiceVM.Vehicle = new VehicleViewModel();
                    ServiceVM.Vehicle.ID = service.Vehicle.ID;
                    ServiceVM.Vehicle.Identifier = service.Vehicle.Identifier;
                    ServiceVM.Vehicle.VehicleOwner = new VehicleOwnerViewModel();
                    ServiceVM.Vehicle.VIN = service.Vehicle.VIN;
                    ServiceVM.Vehicle.VehicleOwner.Email = service.Vehicle.VehicleOwner.Email;
                    ServiceVM.Vehicle.VehicleOwner.FirstName = service.Vehicle.VehicleOwner.FirstName;
                    ServiceVM.Vehicle.VehicleOwner.ID = service.Vehicle.VehicleOwner.ID;
                    ServiceVM.Vehicle.VehicleOwner.LastName = service.Vehicle.VehicleOwner.LastName;
                    ServiceVM.ServiceInterventions = new List<ServiceInterventionViewModel>();

                    if (service.SSI != null)
                    {
                        foreach (var ssi in service.SSI)
                        {
                            ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                            ServiceInterventionVM.Currency = new CurrencyViewModel();
                            ServiceInterventionVM.Currency.ID = ssi.ServiceIntervention.Currency.ID;
                            ServiceInterventionVM.Currency.Name = ssi.ServiceIntervention.Name;
                            ServiceInterventionVM.Flag = ssi.ServiceIntervention.Flag;
                            ServiceInterventionVM.ID = ssi.ServiceIntervention.ID;
                            ServiceInterventionVM.Name = ssi.ServiceIntervention.Name;
                            ServiceInterventionVM.Price = ssi.ServiceIntervention.Price;
                            ServiceVM.ServiceInterventions.Add(ServiceInterventionVM);
                        }
                    }
                    if (service.SE != null)
                    {
                        foreach (var se in service.SE)
                        {
                            EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                            EmployeeVM.Email = se.Employee.Email;
                            EmployeeVM.FirstName = se.Employee.FirstName;
                            EmployeeVM.Flag = se.Employee.Flag;
                            EmployeeVM.ID = se.Employee.ID;
                            EmployeeVM.LastName = se.Employee.PhoneNumber;
                            EmployeeVM.PhoneNumber = se.Employee.PhoneNumber;
                        }
                    }
                    data.Services.Add(ServiceVM);
                }
                
            }
          
            data.ID = WorkingPoint.ID;
            return Json(new { success = success, messages = message, WorkingPoint = data }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult SelectWorkingPoint(WorkingPointViewModel data)
        {
            bool success = false;
            var message = "";
            Session["SelectedWorkingPointId"] = data.ID;

            success = true;
            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditWorkingPoint(WorkingPointViewModel data)
        {
            bool success = false;
            var message = "";
            WorkingPoint wp = new WorkingPoint();
            wp.City = data.City;
            wp.Country = data.Country;
            wp.Nr = data.Nr;
            wp.Street = data.Street;
            wp.ID = data.ID;
            WorkingPointRepository.EditWorkingPoint(wp);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletWorkingPoint(WorkingPointViewModel data)
        {
            bool success = false;
            var message = "";
            WorkingPoint wp = new WorkingPoint();
            wp.ID = data.ID;
            WorkingPointRepository.DeletWorkingPoint(wp);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }



        //SERVICE
        [HttpPost]
        public ActionResult AddService(VehicleViewModel data)
        {

            bool success = false;
            var message = "";
            var vehicle = new Vehicle();
            var serv = new Service();
            vehicle.Identifier = data.Identifier;
            vehicle.VIN = data.VIN;
            vehicle.VehicleOwner = new VehicleOwner();
            vehicle.VehicleOwner.Email = data.VehicleOwner.Email;
            vehicle.VehicleOwner.FirstName = data.VehicleOwner.FirstName;
            vehicle.VehicleOwner.LastName = data.VehicleOwner.LastName;
            vehicle.VehicleOwner.PhoneNumber = data.VehicleOwner.PhoneNumber;

            var uid = User.Identity.GetUserId();
            if ((int)Session["SelectedWorkingPointId"] != -1)
            {
                serv = ServiceRepository.AddService((int)Session["SelectedWorkingPointId"], vehicle);

            }


            data.VehicleOwner.Email = serv.Vehicle.VehicleOwner.Email;
            data.VehicleOwner.FirstName = serv.Vehicle.VehicleOwner.FirstName;
            data.VehicleOwner.PhoneNumber = serv.Vehicle.VehicleOwner.PhoneNumber;
            data.VehicleOwner.LastName = serv.Vehicle.VehicleOwner.LastName;
            data.VehicleOwner.ID = serv.Vehicle.VehicleOwner.ID;
            data.ID = serv.Vehicle.ID;
            ServiceViewModel ServiceVM = new ServiceViewModel();
            ServiceVM.Vehicle = data;
            ServiceVM.CurrentKm = serv.CurrentKm;
            ServiceVM.Flag = serv.Flag;
            ServiceVM.ID = serv.ID;
            ServiceVM.NextVisitDate = serv.NextVisitDate;
            ServiceVM.NextVisitKm = serv.NextVisitKm;
            ServiceVM.Price = serv.Price;
            ServiceVM.ServiceDate = serv.ServiceDate;

            success = true;

            return Json(new { success = success, messages = message, Service = ServiceVM }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletService(ServiceViewModel data)
        {

            bool success = false;
            var message = "";
            Service service = new Service();
            service.ID = data.ID;
            ServiceRepository.DeletService(service);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EndedService(ServiceViewModel data)
        {
            bool success = false;
            var message = "";
            Service service = new Service();
            service.ID = data.ID;
            ServiceRepository.EndedService(service);
            success = true;
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(data.Vehicle.VehicleOwner.Email);
            mailMessage.From = new MailAddress("info.servicebook@gmail.com");
            mailMessage.Subject = "Your vehicle with identifier: " + data.Vehicle.Identifier + " it is ready!";
            Email email = new Email();
            email.Repairs = new List<ServiceInterventionViewModel>();
            email.ServiceVM = data;

            if (data.ServiceInterventions != null)
            {
                foreach (var intervention in data.ServiceInterventions)
                {
                    ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                    ServiceInterventionVM.Name = intervention.Name;
                    ServiceInterventionVM.Price = intervention.Price;
                    email.Repairs.Add(ServiceInterventionVM);
                }
            }
            //mailMessage.Body = "Hello " + data.Vehicle.VehicleOwner.FirstName + " " + data.Vehicle.VehicleOwner.LastName + "!\n\n" + " Your vehicle with identifier: " + data.Vehicle.Identifier + " it is ready! " + " The repairs costs: " + data.Price + " EURO";
            //mailMessage.Body += "\n List of repairs: \n";
            //if (data.ServiceInterventions != null)
            //{
            //    foreach (var intervention in data.ServiceInterventions)
            //    {
            //        mailMessage.Body += intervention.Name;
            //        mailMessage.Body += ", Cost: ";
            //        mailMessage.Body += intervention.Price;
            //        mailMessage.Body += "\n";
            //    }
            //}
            //mailMessage.Body += "\n";
            //mailMessage.Body += "\n Next service date: " + data.NextVisitDate.ToString();
            //mailMessage.Body += "\n Next service km: " + data.NextVisitKm;
            mailMessage.Body += RenderPartialViewToString("_Email", email);


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("info.servicebook@gmail.com", "allamvizsga");

            mailMessage.IsBodyHtml = true;


            smtpClient.Send(mailMessage);

            ////   var results = SMS.Send(new SMS.SMSRequest
            ////   {
            ////      from = Nexmo.Api.Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
            ////      to = "+40" + data.Vehicle.VehicleOwner.PhoneNumber.ToString(),
            ////      text = mailMessage.Body
            ////});

            // Vehicles = Vehicles


            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditService(ServiceViewModel data)
        {
            bool success = false;
            var message = "";
            Service service = new Service();
            service.Vehicle = new Vehicle();
            service.Vehicle.VehicleOwner = new VehicleOwner();
            service.Vehicle.Identifier = data.Vehicle.Identifier;
            service.ID = data.ID;
            service.Vehicle.VIN = data.Vehicle.VIN;
            service.Vehicle.VehicleOwner.Email = data.Vehicle.VehicleOwner.Email;
            service.Vehicle.VehicleOwner.FirstName = data.Vehicle.VehicleOwner.FirstName;
            service.Vehicle.VehicleOwner.LastName = data.Vehicle.VehicleOwner.LastName;
            service.Vehicle.VehicleOwner.PhoneNumber = data.Vehicle.VehicleOwner.PhoneNumber;
            ServiceRepository.EditService(service);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        //Employee
        [HttpPost]
        public ActionResult AddEmployee(EmployeeViewModel data)
        {
            bool success = false;
            var message = "";
            Employee employee = new Employee();
            employee.Email = data.Email;
            employee.FirstName = data.FirstName;
            employee.LastName = data.LastName;
            employee.PhoneNumber = data.PhoneNumber;
            employee.WPID = data.WPID;
            Employee Employee = EmployeeRepository.AddEmployee(employee);
            if (Employee != null)
            {
                success = true;
                data.ID = Employee.ID;
            }
         else
            {
                success = false;
                message = "This Employee is exist!";
            }
            return Json(new { success = success, messages = message, Employee = data }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletEmployee(EmployeeViewModel data)
        {
            bool success = false;
            var message = "";
            Employee employee = new Employee();
            employee.ID = data.ID;
            EmployeeRepository.DeletEmployee(employee);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel data)
        {
            bool success = false;
            var message = "";
            Employee employee = new Employee();
            employee.Email = data.Email;
            employee.FirstName = data.FirstName;
            employee.ID = data.ID;
            employee.LastName = data.LastName;
            employee.PhoneNumber = data.PhoneNumber;
            var emp=EmployeeRepository.EditEmployee(employee);
            if (emp != null)
            {
                success = true;
            }
            else
            {
                success = false;
                message = "This employee is exist!";
            }

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }


        //Interventions
        [HttpPost]
        public ActionResult AddIntervention(ServiceInterventionViewModel data)
        {
            bool success = false;
            var message = "";
            ServiceIntervention serviceIntervention = new ServiceIntervention();
            serviceIntervention.Name = data.Name;
            serviceIntervention.Price = data.Price;
            serviceIntervention.WP = data.WPID;
            ServiceIntervention Intervention = ServiceInterventionRepository.AddIntervention(serviceIntervention);
            if (Intervention != null)
            {
                data.ID = Intervention.ID;
                success = true;
            }
            else
            {
                success = false;
                message = "This intervention is exist!";
            }
          

            return Json(new { success = success, messages = message, Intervention = data }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditIntervention(ServiceInterventionViewModel data)
        {
            bool success = false;
            var message = "";
            ServiceIntervention serviceIntervention = new ServiceIntervention();
            serviceIntervention.Name = data.Name;
            serviceIntervention.ID = data.ID;
            serviceIntervention.Price = data.Price;
            ServiceIntervention editServiceIntervention=ServiceInterventionRepository.EditServiceIntervention(serviceIntervention);
            if(editServiceIntervention!=null)
            {
                success = true;
            }
            else
            {
                success = false;
                message = "This intervention is exist!";
            }

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletIntervention(ServiceInterventionViewModel data)
        {
            bool success = false;
            var message = "";
            ServiceIntervention serviceIntervention = new ServiceIntervention();
            serviceIntervention.ID = data.ID;
            ServiceInterventionRepository.DeletServiceIntervention(serviceIntervention);
            success = true;

            return Json(new { success = success, messages = message }, JsonRequestBehavior.DenyGet);
        }


        //Repairs
        [HttpPost]
        public ActionResult SaveRepairs(ServiceViewModel data)
        {
            bool success = false;
            var message = "";
            Service service = new Service();
            service.CurrentKm = data.CurrentKm;
            service.Flag = data.Flag;
            service.ID = data.ID;
            service.NextVisitDate = data.NextVisitDate;
            service.NextVisitKm = data.NextVisitKm;
            service.Price = data.Price;
            service.ServiceDate = data.ServiceDate;
            service.SE = new List<SE>();
            service.SSI = new List<SSI>();
            foreach (var employee in data.Employees)
            {
                Employee emp = new Employee();
                emp.ID = employee.ID;
                SE se = new SE();
                se.Employee = emp;
                service.SE.Add(se);
            }
            foreach (var intervention in data.ServiceInterventions)
            {
                ServiceIntervention serviceIntervention = new ServiceIntervention();
                serviceIntervention.ID = intervention.ID;
                SSI ssi = new SSI();
                ssi.ServiceIntervention = serviceIntervention;
                service.SSI.Add(ssi);
            }
            double price = ServiceRepository.SaveRepaire(service);
            success = true;
            return Json(new { success = success, messages = message, Price = price }, JsonRequestBehavior.DenyGet);
        }


        //History
        [HttpPost]
        public ActionResult GetVehicleHistory(ServiceViewModel data)
        {
            bool success = false;
            var message = "";
            Vehicle vehicle = new Vehicle();
            
            vehicle.ID = data.Vehicle.ID;
            vehicle.VIN = data.Vehicle.VIN;
            var vehicleHistory = ServiceRepository.GetVehicleHistory(vehicle);
            
            List<ServiceViewModel> VehicleHistory = new List<ServiceViewModel>();
            foreach (var service in vehicleHistory)
            {
                if (service.Flag == 1)
                {
                    ServiceViewModel ServiceVM = new ServiceViewModel();
                    ServiceVM.CompanyName = service.WorkingPoint.VehicleServiceCompany.ServiceName;
                    ServiceVM.WorkingPoint += service.WorkingPoint.Country + " " + service.WorkingPoint.City + " " + service.WorkingPoint.Street + " " + service.WorkingPoint.Nr;
                    ServiceVM.NextVisitDate = service.NextVisitDate;
                    ServiceVM.NextVisitKm = service.NextVisitKm;
                    ServiceVM.ServiceDate = service.ServiceDate;
                    ServiceVM.CurrentKm = service.CurrentKm;
                    ServiceVM.Employees = new List<EmployeeViewModel>();
                    ServiceVM.ServiceInterventions = new List<ServiceInterventionViewModel>();
                    if (service.SE != null)
                    {
                        foreach (var se in service.SE)
                        {
                            EmployeeViewModel EmployeeVM = new EmployeeViewModel();
                            EmployeeVM.FirstName = se.Employee.FirstName;
                            EmployeeVM.LastName = se.Employee.LastName;
                            ServiceVM.Employees.Add(EmployeeVM);
                        }
                    }
                    if (service.SSI != null)
                    {
                        foreach (var ssi in service.SSI)
                        {
                            ServiceInterventionViewModel ServiceInterventionVM = new ServiceInterventionViewModel();
                            ServiceInterventionVM.Name = ssi.ServiceIntervention.Name;
                            ServiceInterventionVM.Price = ssi.ServiceIntervention.Price;
                            ServiceVM.ServiceInterventions.Add(ServiceInterventionVM);
                        }
                    }
                    VehicleHistory.Add(ServiceVM);
                }
            }
            success = true;

            return Json(new { success = success, messages = message, VehicleHistory = VehicleHistory }, JsonRequestBehavior.DenyGet);
        }
        //History
       

        protected string RenderPartialViewToString(string viewName, Email email)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = email;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
