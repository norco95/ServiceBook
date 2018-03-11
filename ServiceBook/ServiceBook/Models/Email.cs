using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.Models
{
    public class Email
    {
        public ServiceViewModel ServiceVM { get; set; }
        public List<ServiceInterventionViewModel> Repairs { get; set; }
    }
}
