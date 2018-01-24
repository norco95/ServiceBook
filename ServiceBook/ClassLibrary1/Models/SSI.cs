using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class SSI
    {
        [Key]
        public int SID { get; set; }
        public int SIID { get; set; }
        [ForeignKey("SID")]
        public virtual Service Service { get; set; }
        [ForeignKey("SIID")]
        public virtual ServiceIntervention ServiceIntervention { get; set; }

    }
}
