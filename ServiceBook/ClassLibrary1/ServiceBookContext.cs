using Microsoft.AspNet.Identity.EntityFramework;
using ServiceBook.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ServiceBook.DAL
{
    public class ServiceBookContext : IdentityDbContext
    {
        public ServiceBookContext() : base("name=ServiceBookContext")
        {
            Database.SetInitializer(new ServiceBookInitializer());
        }

        public DbSet<VehicleOwner> VehicleOwner { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<SSI> SSI { get; set; }

        public DbSet<SE> SE { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceIntervention> ServiceIntervention { get; set; }
        public DbSet<VehicleServiceCompanyOwner> VehicleServiceCompanyOwner { get; set; }
        public DbSet<VehicleServiceCompany> VehicleServiceCompan { get; set; }
       
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<WorkingPoint> WorkingPoint { get; set; }

        public object UserDetails { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Empty, ex);
            }
        }
    }
}
