using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class CCO
    {
        [Key]
        public int CID { get; set; }

        public int COID { get; set; }

        [ForeignKey("CID")]
        public virtual VehicleServiceCompany VehicleServiceCompany{ get; set;}

        [ForeignKey("COID")]
        public virtual VehicleServiceCompanyOwner VehicleServiceCompanyOwner { get; set; }




    }
}
