using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class VehicleServiceCompanyOwnerRepository
    {
        private ServiceBookContext ServiceBookContext = new ServiceBookContext();
        private WorkingPointRepository WorkingPointRepository = new WorkingPointRepository();
        public List<VehicleServiceCompany> GetVehicleCompanies(string uid)
        {
            List<VehicleServiceCompany> VehicleServiceCompanies = new List<VehicleServiceCompany>();
            VehicleServiceCompanyOwner CurrentOwner=ServiceBookContext.VehicleServiceCompanyOwner.FirstOrDefault(x => x.UID == uid);
            foreach(var cco in CurrentOwner.CCO)
            {
                VehicleServiceCompanies.Add(cco.VehicleServiceCompany);
            }
            return VehicleServiceCompanies;
        }
        public void DeletVehicleServiceCompany(string uid,VehicleServiceCompany VehicleServiceCompany)
        {
            VehicleServiceCompanyOwner CurrentOwner = ServiceBookContext.VehicleServiceCompanyOwner.FirstOrDefault(x => x.UID == uid);
            CCO deletVehicleServiceCompany = ServiceBookContext.CCO.FirstOrDefault(x => x.COID == CurrentOwner.ID && x.CID == VehicleServiceCompany.ID);
           
            
            if (deletVehicleServiceCompany.VehicleServiceCompany != null)
            {
                if(deletVehicleServiceCompany.VehicleServiceCompany.WorkingPoints!=null)
                {
                    foreach(var workingPoint in deletVehicleServiceCompany.VehicleServiceCompany.WorkingPoints)
                    {
                        WorkingPointRepository.DeletWorkingPoint(workingPoint);
                    }
                } 
            }
            ServiceBookContext.CCO.Remove(deletVehicleServiceCompany);
            ServiceBookContext.SaveChanges();
        }

        public VehicleServiceCompany GetCompany(int id)
        {
            return ServiceBookContext.VehicleServiceCompany.FirstOrDefault(x => x.ID == id);
        }
    }
}
