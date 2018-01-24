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
    public class VehicleServiceCompany
    {
        [Key]
        public int ID { get; set; }

        public string ServiceName { get; set; }

        
        public string UID { get; set; }
        [ForeignKey("UID")]
        public virtual IdentityUser IdentityUser { get; set; }

        public int VSCOID { get; set; }
        [ForeignKey("VSCOID")]
        public virtual VehicleServiceCompanyOwner VehicleServiceCompanyOwner { get; set; }
        public virtual ICollection<WorkingPoint> WorkingPoints { get; set; }

    }
}
