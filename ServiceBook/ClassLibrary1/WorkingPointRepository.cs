using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class WorkingPointRepository
    {
        private ServiceBookContext ServiceBookContext = new ServiceBookContext();
        public WorkingPoint AddWorkingPoint(WorkingPoint WorkingPoint)
        {
            VehicleServiceCompany CurrentService= ServiceBookContext.VehicleServiceCompany.FirstOrDefault(x => x.ID == WorkingPoint.VSCID);
            WorkingPoint workingpoint = ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.City == WorkingPoint.City && x.Country == WorkingPoint.Country && x.Street == WorkingPoint.Street && x.Nr == WorkingPoint.Nr);
           
            if (workingpoint != null)
            {
                if (workingpoint.Flag == 0)
                {
                    return null;
                }
                else
                {
                    
                    workingpoint.VehicleServiceCompany = CurrentService;
                    workingpoint.Flag = 0;
                    ServiceBookContext.SaveChanges();
                    WorkingPoint=workingpoint;
                }
            }
            else
            {
                WorkingPoint.VehicleServiceCompany = CurrentService;
                WorkingPoint.Flag = 0;
                ServiceBookContext.WorkingPoint.Add(WorkingPoint);
                ServiceBookContext.SaveChanges();
            }
           
            
          
            return WorkingPoint;
        }
        public void DeletWorkingPoint(WorkingPoint WorkingPoint)
        {
            WorkingPoint DeletWorkingPoint =ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == WorkingPoint.ID);
            DeletWorkingPoint.Flag = 1;
            ServiceBookContext.SaveChanges();
        }
        public void EditWorkingPoint(WorkingPoint WorkingPoint)
        {
            WorkingPoint workingPoint=ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == WorkingPoint.ID);
            workingPoint.Nr = WorkingPoint.Nr;
            workingPoint.Street = WorkingPoint.Street;
            workingPoint.City = WorkingPoint.City;
            workingPoint.Country = WorkingPoint.Country;
            ServiceBookContext.SaveChanges();
        }

        public WorkingPoint GetWorkingPoint(int id)
        {
            return ServiceBookContext.WorkingPoint.FirstOrDefault(x=>x.ID==id);
        }
        public int GetWorkingPointCount()
        {
            return ServiceBookContext.WorkingPoint.Where(x=>x.Flag==0).Count();
        }
    }
}
