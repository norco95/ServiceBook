using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class VehicleRepository
    {
        ServiceBookContext ServiceBookContext = new ServiceBookContext();
        ServiceRepository ServiceRepository = new ServiceRepository();

        public int GetVehicleCount()
        {
            int n=ServiceBookContext.Vehicle.Count();
            return n;
        }

        public Vehicle GetVehicleByVIN(string vin)
        {
            return ServiceBookContext.Vehicle.FirstOrDefault(x => x.VIN == vin);
        }

        public List<Vehicle> getAllVehicle()
        {
            return ServiceBookContext.Vehicle.ToList();
        }
        //public List<Vehicle> getVehiclesByWorkingPoint(int id)
        //{
        //    List<Vehicle> vehicles = new List<Vehicle>();
        //    var sw= ServiceBookContext.SW.Where(x => x.WorkingPoint.ID == id && x.Service.Flag == 0);
        //    foreach(var s in sw)
        //    {
        //        vehicles.Add(s.Service.Vehicle);
        //    }

        //    return vehicles;
        //} 
    }
}
