using Microsoft.AspNet.Identity.EntityFramework;
using ServiceBook.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Modles
{
    public class VehicleServiceCompanyOwner
    {
        
        public int ID { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UID { get; set; }
    
        public List<VehicleServiceCompanyViewModel> VehicleServiceCompanies { get; set; }
    }
}
