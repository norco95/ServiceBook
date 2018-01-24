using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int WPID { get; set; }

        [ForeignKey("WPID")]
        public virtual WorkingPoint WorkingPoint { get; set; }

        public virtual ICollection<SE> SE { get; set; }
    }
}
