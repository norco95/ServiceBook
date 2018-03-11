using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Models
{
    public class ServiceViewModel
    {
      
        public int ID { get; set; }
        public DateTime ServiceDate { get; set; }
        public int Flag { get; set; }
        public DateTime NextVisitDate{ get; set; }
        public int CurrentKm { get; set; }
        public int NextVisitKm { get; set; }
        public string WorkingPoint { get; set; }
        public string CompanyName { get; set; }
        public double Price { get; set; }
        public VehicleViewModel Vehicle { get; set; }
        public List<ServiceInterventionViewModel> ServiceInterventions { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
        

    }
}
