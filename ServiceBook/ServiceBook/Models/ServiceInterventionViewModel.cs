using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Models
{
   public class ServiceInterventionViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public int WPID { get; set; }
        
        public int Flag { get; set; }
        public CurrencyViewModel Currency { get; set; } 
      
    }
}
