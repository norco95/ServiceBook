using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceBook.Models
{
    public class HomeViewModel
    {
        public int Workingpoints { get; set; }
        public int Services { get; set; }
        public int Vehicles { get; set; }
        public int Reviews { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}