using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceBook.DAL
{
    public class ServiceBookInitializer : IDatabaseInitializer<ServiceBookContext>
    {
        public void InitializeDatabase(ServiceBookContext context)
        {
        }
    }
}
