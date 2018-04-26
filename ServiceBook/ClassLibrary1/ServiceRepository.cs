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
      
        public List<Service> GetListOfServies(DateTime nextVisitDate)
        {
            var services = ServiceBookContext.Service.Where(x => x.NextVisitDate.Year == nextVisitDate.Year && x.NextVisitDate.Month == nextVisitDate.Month && x.NextVisitDate.Day == nextVisitDate.Day);
            return services.ToList();
        }

        public double SaveRepaire(Service Service)
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
            service.NextVisitDate = Service.NextVisitDate;
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
            return price;
        }

        public Service AddService(int WorkingPointId, Vehicle Vehicle)
        {
            var WorkingPoint = ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == WorkingPointId);
            var vehicle = ServiceBookContext.Vehicle.FirstOrDefault(x => x.VIN == Vehicle.VIN);
            var owner = ServiceBookContext.VehicleOwner.FirstOrDefault(x => x.Email == Vehicle.VehicleOwner.Email && x.PhoneNumber == Vehicle.VehicleOwner.PhoneNumber);

            Service s = new Service();
            if (vehicle != null)
            {
                s.Vehicle = vehicle;
                s.NextVisitKm = vehicle.Services.Max(x => x.CurrentKm);
                s.CurrentKm = vehicle.Services.Max(x => x.CurrentKm);
            }
            else
            {
                s.Vehicle = Vehicle;
                s.NextVisitKm = 0;
                s.CurrentKm = 0;
            }
            if (owner != null)
            {
                s.Vehicle.VehicleOwner = owner;
            }
            s.Flag = 0;
          
            s.NextVisitDate = DateTime.Now;
            s.ServiceDate = DateTime.Now;
            WorkingPoint.Services.Add(s);
            
            ServiceBookContext.SaveChanges();

            return s;
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

            List<Service> History = new List<Service>();
            var vehicle = new Vehicle();

            if (Vehicle.ID != null)
            {
                vehicle = ServiceBookContext.Vehicle.FirstOrDefault(x => x.ID == Vehicle.ID);
            }
            if(Vehicle.VIN!=null)
            {
                vehicle = ServiceBookContext.Vehicle.FirstOrDefault(x => x.VIN == Vehicle.VIN);
            }
            
            History = vehicle.Services.Reverse().ToList();
            return History;
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