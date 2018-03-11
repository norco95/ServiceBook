using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        public int Rate { get; set; } 
        public String Description { get; set; }
        public int SID { get; set; }
        [ForeignKey("SID")]
        public virtual Service Service{get;set;}
     
    }
}
