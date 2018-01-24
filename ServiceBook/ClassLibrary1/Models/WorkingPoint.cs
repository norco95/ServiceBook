using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class WorkingPoint
    {
        [Key]
        public int ID { get; set; }
        public string Country { get; set; }

        public string City { get; set; }
        
        public string Street { get; set; }
        
        public string Nr { get; set; }  
        public int VSOCID { get; set; }
        [ForeignKey("VSOCID")]
        public virtual VehicleServiceCompanyOwner VehicleServiceCompanyOwner { get; set; }
        public virtual ICollection<Employee> Employess { get; set; }

        public virtual ICollection<ServiceIntervention> ServiceImterventions { get; set; }

    }
}
