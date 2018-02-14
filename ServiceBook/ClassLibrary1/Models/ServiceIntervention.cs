using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
   public class ServiceIntervention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        
        public int Flag { get; set; }

        public int CID { get; set; }

        [ForeignKey("CID")]
        public virtual Currency Currency { get; set; }

        public int? WP { get; set; }
        [ForeignKey("WP")]
        public virtual WorkingPoint WorkingPoint { get; set; }

        public virtual ICollection<SSI> SSI { get; set; }
    }
}
