using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceBook.Models
{
    public class AroundKmViewModel
    {
        public int LastKm{ get; set;}
        public int AroundKm { get; set; }
        public bool Exist { get; set; }
        public DateTime LastRepairDate { get; set; }
    }
}