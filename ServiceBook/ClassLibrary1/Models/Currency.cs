﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL.Models
{
    public class Currency
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ServiceIntervention> ServiceIntervention { get; set; }
    }
}
