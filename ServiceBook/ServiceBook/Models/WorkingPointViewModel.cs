using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Models
{
    public class WorkingPointViewModel
    {

        public int ID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Flag { get; set; }
        public string Street { get; set; }     
        public string Nr { get; set; }  
        public int VSCID { get; set; }
        public string CompanyName { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }

        public List<ServiceInterventionViewModel> ServiceInterventions { get; set; }
        public List<ServiceViewModel> Services { get; set; }

        public double Rate { get; set; }

        public List<String> Reviews { get; set; }
        

    }
}
