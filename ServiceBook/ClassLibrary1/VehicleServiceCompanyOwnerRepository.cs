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
            if (CurrentOwner.CCO != null)
            {
                foreach (var cco in CurrentOwner.CCO.ToList())
                {
                    if (cco != null)
                    {
                        if (cco.VehicleServiceCompany != null)
                        {
                            if (cco.VehicleServiceCompany.CCO != null)
                            {
                                cco.VehicleServiceCompany.CCO = null;
                            }
                            if (cco.VehicleServiceCompany.WorkingPoints != null)
                            {
                                foreach (var WorkingPoint in cco.VehicleServiceCompany.WorkingPoints.ToList())
                                {
                                   
                                    if(WorkingPoint.Employees!=null)
                                    {
                                        foreach(var employee in WorkingPoint.Employees.ToList())
                                        {
                                            if(employee.WorkingPoint!=null)
                                            {
                                                employee.WorkingPoint = null;
                                            }
                                            if (employee.SE != null)
                                            {
                                                employee.SE = null;
                                            }
                                            if(employee.Flag!=0)
                                            {
                                                WorkingPoint.Employees.Remove(employee);
                                            }
                                        }
                                    }

                                    if (WorkingPoint.ServiceInterventions != null)
                                    {
                                        foreach (var ServiceIntervention in WorkingPoint.ServiceInterventions.ToList())
                                        {
                                            
                                            if (ServiceIntervention.WorkingPoint != null)
                                            {
                                                ServiceIntervention.WorkingPoint = null;
                                            }
                                            if (ServiceIntervention.Currency != null)
                                            {
                                                if (ServiceIntervention.Currency.ServiceIntervention != null)
                                                {
                                                    ServiceIntervention.Currency.ServiceIntervention = null;
                                                }
                                            }
                                            if (ServiceIntervention.Flag != 0)
                                            {
                                                WorkingPoint.ServiceInterventions.Remove(ServiceIntervention);
                                            }
                                        }
                                    }
                                    if (WorkingPoint.VehicleServiceCompany != null)
                                    {
                                        WorkingPoint.VehicleServiceCompany = null;
                                    }
                                    if (WorkingPoint.SW != null)
                                    {
                                        
                                       foreach (var sw in WorkingPoint.SW.ToList())
                                        {
                                            if (sw.Service != null && sw.Service.SSI!=null)
                                            { 
                                                foreach (var ssi in sw.Service.SSI)
                                                {
                                                    if (ssi.Service != null)
                                                    {
                                                        ssi.Service = null;
                                                    }
                                                  
                                                }
                                            }
                                            if(sw.WorkingPoint!=null)
                                            {
                                                sw.WorkingPoint = null;
                                            }
                                            if (sw.Service.SW != null)
                                            {
                                                foreach(var ssw in sw.Service.SW)
                                                {
                                                    if (ssw.Service != null)
                                                    {
                                                        if(ssw.Service.SE!=null)
                                                        {
                                                            foreach(var se in ssw.Service.SE)
                                                            {
                                                                if(se.Service!=null)
                                                                {
                                                                    se.Service = null;
                                                                }
                                                                if (se.Employee != null)
                                                                {
                                                                    if (se.Employee.SE != null)
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
                                                        if (ssw.Service.SSI != null)
                                                        {
                                                            foreach(var sssi in ssw.Service.SSI)
                                                            {
                                                                if(sssi.Service!=null)
                                                                {
                                                                    sssi.Service = null;
                                                                }
                                                                if(sssi.ServiceIntervention!=null)
                                                                {
                                                                    if (sssi.ServiceIntervention.Currency != null && sssi.ServiceIntervention.Currency.ServiceIntervention!=null)
                                                                    {

                                                                        sssi.ServiceIntervention.Currency.ServiceIntervention = null;
                                                                    }
                                                                    if(sssi.ServiceIntervention.SSI!=null)
                                                                    {
                                                                        sssi.ServiceIntervention.SSI = null;
                                                                    }
                                                                    if(sssi.ServiceIntervention.WorkingPoint!=null)
                                                                    {
                                                                        sssi.ServiceIntervention.WorkingPoint = null;
                                                                    }
                                                                }
                                                                
                                                            }
                                                        }
                                                        if(ssw.Service.SW!=null)
                                                        {
                                                            ssw.Service.SW = null;
                                                        }
                                                        if(ssw.Service.Vehicle!=null)
                                                        { 
                                                            if(ssw.Service.Vehicle.Services!=null)
                                                            {
                                                                ssw.Service.Vehicle.Services = null;
                                                            }
                                                            if(ssw.Service.Vehicle.VehicleOwner!=null)
                                                            {
                                                                ssw.Service.Vehicle.VehicleOwner.Vehicles = null;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if(sw.Service.Flag!=0)
                                            {
                                                WorkingPoint.SW.Remove(sw);
                                            }
                                        }
                                    }
                                    if (WorkingPoint.Flag != 0)
                                    {
                                        cco.VehicleServiceCompany.WorkingPoints.Remove(WorkingPoint);
                                    }
                                }
                            }
                            
                            if (cco.VehicleServiceCompanyOwner!=null && cco.VehicleServiceCompanyOwner.CCO != null)
                            {
                                cco.VehicleServiceCompanyOwner.CCO = null;
                            }
                        }
                    }
                    if (cco.VehicleServiceCompany.Flag == 0)
                    {
                        VehicleServiceCompanies.Add(cco.VehicleServiceCompany);
                    }
                    
                }
            }
            return VehicleServiceCompanies;
        }
        public void DeletVehicleServiceCompany(string uid,VehicleServiceCompany VehicleServiceCompany)
        {
            VehicleServiceCompanyOwner CurrentOwner = ServiceBookContext.VehicleServiceCompanyOwner.FirstOrDefault(x => x.UID == uid);
            CCO deletVehicleServiceCompany = ServiceBookContext.CCO.FirstOrDefault(x => x.COID == CurrentOwner.ID && x.CID == VehicleServiceCompany.ID);
            deletVehicleServiceCompany.VehicleServiceCompany.Flag = 1;
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
            ServiceBookContext.SaveChanges();
        }
    }
}
