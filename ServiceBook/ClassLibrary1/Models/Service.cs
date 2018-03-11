using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime ServiceDate { get; set; }
        public int Flag { get; set; }
        public DateTime NextVisitDate{ get; set; }
        public int CurrentKm { get; set; }
        public int NextVisitKm { get; set; }
        public double Price { get; set; }
        public int VID { get; set; }
        public int WPID { get; set; }
        public int? RID { get; set; }
        

        [ForeignKey("VID")]
        public virtual Vehicle Vehicle { get; set; }
        [ForeignKey("WPID")]
        public virtual WorkingPoint WorkingPoint{ get ;set; }

        [ForeignKey("RID")]
        public virtual Review Review { get; set; }
        public virtual ICollection<SSI> SSI { get; set; }
        public virtual ICollection<SE> SE { get; set; }
       


    }
}
