using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ServiceBook.DAL.Models
{
    public class VehicleServiceCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string ServiceName { get; set; }

        public int Flag { get; set; }

       
        public virtual ICollection<CCO> CCO { get; set; }
        
        public virtual ICollection<WorkingPoint> WorkingPoints { get; set; }

    }
}
