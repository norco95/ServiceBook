using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{

    public class ServiceRepository
    {
        private ServiceBookContext ServiceBookContext = new ServiceBookContext();
      

        public Service SaveRepaire(Service Service)
        {
           
            Service service = ServiceBookContext.Service.FirstOrDefault(x => x.ID == Service.ID);

            if(service!=null)
            {
                if(service.SSI!=null)
                {
                    foreach(var ssi in service.SSI.ToList())
                    {
                        ServiceBookContext.SSI.Remove(ssi);
                    }
                }
            }

            if (service != null)
            {
                if (service.SE != null)
                {
                    foreach (var se in service.SE.ToList())
                    {
                        ServiceBookContext.SE.Remove(se);
                    }
                }
            }
            
            double price = 0;
            service.NextVisitKm = Service.NextVisitKm;
            service.NextVisitDate = DateTime.Now; //Service.NextVisitDate;
            service.CurrentKm = Service.CurrentKm;
            service.ServiceDate = DateTime.Now;
            if (Service.SSI != null)
            {
                foreach (var ssi in Service.SSI)
                {
                    ssi.Service = service;
                    ServiceIntervention serviceIntervention = ServiceBookContext.ServiceIntervention.FirstOrDefault(x => x.ID == ssi.ServiceIntervention.ID);
                    if (serviceIntervention != null)
                    {
                        ssi.ServiceIntervention = serviceIntervention;
                        price += ssi.ServiceIntervention.Price;
                        ServiceBookContext.SSI.Add(ssi);
                       
                    }
                }
            }
            service.Price = price;

            if (Service.SE != null)
            {
                foreach (var se in Service.SE)
                {
                    se.Service = service;
                    Employee Employee= ServiceBookContext.Employee.FirstOrDefault(x => x.ID == se.Employee.ID);
                    if (Employee != null)
                    {
                        se.Employee = Employee;
                        ServiceBookContext.SE.Add(se);
                    
                    }
                }
            }

            ServiceBookContext.SaveChanges();

            if(service.SW!=null)
            {
                service.SW = null;
            }
            if (service.SE != null)
            {
                foreach(var se in service.SE)
                {
                    if(se.Service!=null)
                    {
                        se.Service = null;
                    }
                    if(se.Employee!=null)
                    {
                        if(se.Employee.WorkingPoint!=null)
                        {
                            se.Employee.WorkingPoint = null;
                        }
                        if (se.Employee.SE != null)
                        {
                            se.Employee.SE = null;
                        }
                       
                    }
                }
               
            }
            if(service.SSI!=null)
            {
                foreach(var ssi in service.SSI)
                {
                    if(ssi.Service!=null)
                    {
                        ssi.Service = null;
                    }
                    if(ssi.ServiceIntervention!=null)
                    {
                        if(ssi.ServiceIntervention.SSI!=null)
                        {
                            ssi.ServiceIntervention.SSI = null;
                        }
                        if(ssi.ServiceIntervention.Currency!=null)
                        {
                            ssi.ServiceIntervention.Currency = null;
                        }
                        if(ssi.ServiceIntervention.WorkingPoint!=null)
                        {
                            ssi.ServiceIntervention.WorkingPoint = null;
                        }
                        
                    }
                }
            }
            if(service.Vehicle!=null)

            {
                service.Vehicle = null;
            }

            return service;
        }

        public SW AddService(int WorkingPointId, Vehicle Vehicle)
        {
            var WorkingPoint = ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == WorkingPointId);
            var vehicle = ServiceBookContext.Vehicle.FirstOrDefault(x => x.VIN == Vehicle.VIN);
            SW sw = new SW();
            sw.WorkingPoint = WorkingPoint;
            sw.Service = new Service();
            if (vehicle != null)
            {
                sw.Service.Vehicle = vehicle;
            }
            else
            {
                sw.Service.Vehicle = Vehicle;
            }
            sw.Service.Flag = 0;
            sw.Service.NextVisitKm = 0;
            sw.Service.CurrentKm = 0;
            sw.Service.NextVisitDate = DateTime.Now;
            sw.Service.ServiceDate = DateTime.Now;
            
            ServiceBookContext.SW.Add(sw);
            ServiceBookContext.SaveChanges();
            if (sw.Service != null)
            {
                if (sw.Service.SE != null)
                {
                    sw.Service.SE = null;
                }
                if (sw.Service.SSI != null)
                {
                    sw.Service.SSI = null;
                }
                if (sw.Service.SW != null)
                {
                    sw.Service.SW = null;
                }
                if (sw.Service.Vehicle != null)
                {
                    if (sw.Service.Vehicle.Services != null)
                    {
                        sw.Service.Vehicle.Services = null;
                    }
                    if (sw.Service.Vehicle.VehicleOwner != null)
                    {
                        if (sw.Service.Vehicle.VehicleOwner.Vehicles != null)
                        {
                            sw.Service.Vehicle.VehicleOwner.Vehicles = null;
                        }
                    }

                }
            }
            if (sw.WorkingPoint != null)
            {
                sw.WorkingPoint = null;
            }
            return sw;

        }

        public void DeletService(Service Service)
        {
            Service DeletService = ServiceBookContext.Service.FirstOrDefault(x => x.ID == Service.ID);
            DeletService.Flag = 2;
            ServiceBookContext.SaveChanges();

        }

        public void EndedService(Service Service)
        {
            Service EndedService = ServiceBookContext.Service.FirstOrDefault(x => x.ID == Service.ID);
            EndedService.Flag = 1;
            ServiceBookContext.SaveChanges();
        }

        public List<Service> GetVehicleHistory(Vehicle Vehicle)
        {

            var VehicleHisotry = ServiceBookContext.Vehicle.FirstOrDefault(x => x.ID == Vehicle.ID);
            var services = VehicleHisotry.Services.Where(x => x.Flag == 1);

            List<Service> VehicleHisotries = new List<Service>();
            foreach (var history in services.ToList())
            {
               
                if(history.Vehicle!=null)
                {
                    history.Vehicle.Services = null;
                    if(history.Vehicle.VehicleOwner!=null)
                    {
                        history.Vehicle.VehicleOwner.Vehicles = null;
                    }
                    
                }
                if (history.SW != null)
                {
                    foreach(var sw in history.SW)
                    {
                        if(sw.Service!=null)
                        {
                            sw.Service = null;
                        }
                        if(sw.WorkingPoint!=null)
                        {
                            if(sw.WorkingPoint.Employees!=null)
                            {
                                sw.WorkingPoint.Employees = null;
                            }
                            if(sw.WorkingPoint.ServiceInterventions!=null)
                            {
                                sw.WorkingPoint.ServiceInterventions = null;
                            }
                            if(sw.WorkingPoint.SW!=null)
                            {
                                sw.WorkingPoint.SW = null;
                            }
                            if (sw.WorkingPoint.VehicleServiceCompany.WorkingPoints != null)
                            {
                                sw.WorkingPoint.VehicleServiceCompany.WorkingPoints = null;
                            }
                            if(sw.WorkingPoint.VehicleServiceCompany.CCO!=null)
                            {
                                sw.WorkingPoint.VehicleServiceCompany.CCO = null;
                            }
                        }
                    }
                }
                if (history.SE != null)
                {
                   foreach(var se in history.SE)
                    {
                        if (se.Service != null)
                        {
                            se.Service = null;
                        }
                        if(se.Employee!=null)
                        {
                            if(se.Employee.SE!=null)
                            {
                                se.Employee.SE = null;
                            }
                            if(se.Employee.WorkingPoint!=null)
                            {
                                se.Employee.WorkingPoint = null;
                            }
                        }
                    }
                }
                if (history.SSI != null)
                {
                    foreach(var ssi in history.SSI)
                    {
                        if (ssi.ServiceIntervention != null)
                        {
                            if (ssi.ServiceIntervention.SSI != null)
                            {
                                ssi.ServiceIntervention.SSI = null;
                            }
                            if (ssi.ServiceIntervention.WorkingPoint != null)
                            {
                                ssi.ServiceIntervention.WorkingPoint = null;
                            }
                            if (ssi.ServiceIntervention.Currency != null)
                            {
                                ssi.ServiceIntervention.Currency = null;
                            }
                            
                        }
                        
                        ssi.Service = null;
                    }

                }
                
                VehicleHisotries.Add(history);
            }


            return VehicleHisotries;
        }

        public void EditService(Service Service)
        {
            Service service=ServiceBookContext.Service.FirstOrDefault(x => x.ID == Service.ID);
            service.Vehicle.Identifier = Service.Vehicle.Identifier;
            service.Vehicle.VIN = Service.Vehicle.VIN;
            service.Vehicle.VehicleOwner.LastName = Service.Vehicle.VehicleOwner.LastName;
            service.Vehicle.VehicleOwner.FirstName = Service.Vehicle.VehicleOwner.FirstName;
            service.Vehicle.VehicleOwner.Email = Service.Vehicle.VehicleOwner.Email;
            service.Vehicle.VehicleOwner.PhoneNumber = Service.Vehicle.VehicleOwner.PhoneNumber;
            ServiceBookContext.SaveChanges();
        }
    }
}