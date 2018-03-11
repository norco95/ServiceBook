using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Models
{
    public class VehicleViewModel
    {
      
        public int ID { get; set; }
        public string VIN { get; set; }
        public string Identifier { get; set; }
      
        public VehicleOwnerViewModel VehicleOwner{ get; set; }

        
        

    }
}
