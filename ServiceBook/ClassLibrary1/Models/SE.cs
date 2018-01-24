using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class SE
    {
        [Key]
        public int SID { get; set; }
        public int EID { get; set; }
        [ForeignKey("SID")]
        public virtual Service Service { get; set; }
        [ForeignKey("EID")]
        public virtual Employee Employee { get; set; }
    }
}
