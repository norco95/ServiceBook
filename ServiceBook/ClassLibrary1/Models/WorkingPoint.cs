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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Country { get; set; }

        public string City { get; set; }
        public int Flag { get; set; }
        public string Street { get; set; }
        
        public string Nr { get; set; }  
        public int VSCID { get; set; }
        [ForeignKey("VSCID")]
        public virtual VehicleServiceCompany VehicleServiceCompany { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<ServiceIntervention> ServiceInterventions { get; set; }
        public virtual ICollection<Service> Services { get; set; }

    }
}
