using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class VehicleServiceCompanyOwner
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public virtual ICollection<VehicleServiceCompany> VehicleServiceCompanys { get; set; }
    }
}
