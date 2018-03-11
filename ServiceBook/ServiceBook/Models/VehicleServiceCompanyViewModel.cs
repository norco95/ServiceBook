using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ServiceBook.Models
{
    public class VehicleServiceCompanyViewModel
    {
       
        public int ID { get; set; }

        public string ServiceName { get; set; }

        public int Flag { get; set; }
        
        public List<WorkingPointViewModel> WorkingPoints { get; set; }

    }
}
