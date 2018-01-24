using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        public string VIN { get; set; }
        
        public int OID { get; set; }
        [ForeignKey("OID")]
        public virtual VehicleOwner VehicleOwner{ get; set; }

        public virtual ICollection<Service> Services { get; set; }

        

    }
}
