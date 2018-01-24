﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceBook.DAL.Models
{
   public class VehicleOwner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Key]
        public int ID { get; set; }
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        

        
    }
}
