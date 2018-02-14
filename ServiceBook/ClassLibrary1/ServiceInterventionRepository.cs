using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class ServiceInterventionRepository
    {
        private ServiceBookContext ServiceBookContext = new ServiceBookContext();
        public ServiceIntervention AddIntervention(ServiceIntervention ServiceIntervention)
        {
            WorkingPoint WorkingPoint=ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == ServiceIntervention.WorkingPoint.ID);
            ServiceIntervention.WorkingPoint = WorkingPoint;
            ServiceIntervention.Currency = new Currency();
            ServiceIntervention.Currency.Name = "Euro";
            ServiceIntervention.Flag = 0;
            ServiceBookContext.ServiceIntervention.Add(ServiceIntervention);
            ServiceBookContext.SaveChanges();


            if (ServiceIntervention.WorkingPoint != null)
            {
                if (ServiceIntervention.WorkingPoint.ServiceInterventions != null)
                {
                    ServiceIntervention.WorkingPoint.ServiceInterventions = null;
                }
                if (ServiceIntervention.Currency.ServiceIntervention != null)
                {
                    ServiceIntervention.Currency.ServiceIntervention = null;
                }
                if (ServiceIntervention.WorkingPoint.VehicleServiceCompany.WorkingPoints != null)
                {
                    ServiceIntervention.WorkingPoint.VehicleServiceCompany.WorkingPoints = null;

                }

                if (ServiceIntervention.WorkingPoint.SW != null)
                {
                    ServiceIntervention.WorkingPoint.SW = null;
                }
                if (ServiceIntervention.WorkingPoint.VehicleServiceCompany != null)
                {
                    ServiceIntervention.WorkingPoint.VehicleServiceCompany = null;
                }
                if(ServiceIntervention.WorkingPoint.Employees!=null)
                {
                    ServiceIntervention.WorkingPoint.Employees = null;
                }
            }
            return ServiceIntervention;
        }
        public void EditServiceIntervention(ServiceIntervention ServiceIntervention)
        {
            ServiceIntervention EditServiceIntervention = ServiceBookContext.ServiceIntervention.FirstOrDefault(x => x.ID == ServiceIntervention.ID);
            EditServiceIntervention.Name = ServiceIntervention.Name;
            EditServiceIntervention.Price = ServiceIntervention.Price;
            ServiceBookContext.SaveChanges();

        }

        public void DeletServiceIntervention(ServiceIntervention ServiceIntervention)
        {
            ServiceIntervention DeletServiceIntervention = ServiceBookContext.ServiceIntervention.FirstOrDefault(x => x.ID == ServiceIntervention.ID);
            DeletServiceIntervention.Flag = 1;
            ServiceBookContext.SaveChanges();
        }
    }
}
