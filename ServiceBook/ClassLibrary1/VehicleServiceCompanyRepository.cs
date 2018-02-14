using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class VehicleServiceCompanyRepository
    {
        private ServiceBookContext ServiceBookContext = new ServiceBookContext();
        public int EditServiceCompany(VehicleServiceCompany VehicleServiceCompany)
        {
            VehicleServiceCompany vehicleServiceCompany=ServiceBookContext.VehicleServiceCompany.FirstOrDefault(x => x.ServiceName == VehicleServiceCompany.ServiceName);
            if (vehicleServiceCompany == null)
            {
                vehicleServiceCompany = ServiceBookContext.VehicleServiceCompany.FirstOrDefault(x => x.ID == VehicleServiceCompany.ID);
                vehicleServiceCompany.ServiceName = VehicleServiceCompany.ServiceName;
                ServiceBookContext.SaveChanges();
                return 1;
            }
           
            return 0;
        }
        public CCO AddVehicleServiceCompany(string uid, VehicleServiceCompany VehicleServiceCompany)
        {
            VehicleServiceCompanyOwner CurrentOwner = ServiceBookContext.VehicleServiceCompanyOwner.FirstOrDefault(x => x.UID == uid);
            VehicleServiceCompany Company = ServiceBookContext.VehicleServiceCompany.FirstOrDefault(x => x.ServiceName == VehicleServiceCompany.ServiceName);
            var CCO = ServiceBookContext.CCO.FirstOrDefault(x => x.VehicleServiceCompanyOwner.ID == CurrentOwner.ID && x.VehicleServiceCompany.ServiceName == VehicleServiceCompany.ServiceName);
            CCO cco = new CCO();
            if (CCO == null)
            {
                
                cco.VehicleServiceCompanyOwner = CurrentOwner;
                if (Company != null)
                {
                    cco.VehicleServiceCompany = Company;
                }
                else
                {
                    cco.VehicleServiceCompany = VehicleServiceCompany;
                }
                cco.VehicleServiceCompany.Flag = 0;

                ServiceBookContext.CCO.Add(cco);
                ServiceBookContext.SaveChanges();
               
            }
            else
            {
                if(CCO.VehicleServiceCompany.Flag==0)
                {
                    return null;
                }
                CCO.VehicleServiceCompany.Flag = 0;
                ServiceBookContext.SaveChanges();
                cco = CCO;
            }

           
            if (cco.VehicleServiceCompany != null)
            {
                if (cco.VehicleServiceCompany.WorkingPoints != null)
                {
                    cco.VehicleServiceCompany.WorkingPoints = null;
                }
                if (cco.VehicleServiceCompany.CCO != null)
                {
                    cco.VehicleServiceCompany.CCO = null;
                }
                if (cco.VehicleServiceCompanyOwner != null)
                {
                    cco.VehicleServiceCompanyOwner = null;
                }
            }
            return cco;
        }

    }
}
