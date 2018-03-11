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
        public DbSet<VehicleServiceCompany> VehicleServiceCompany { get; set; }
        public DbSet<CCO> CCO { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<WorkingPoint> WorkingPoint { get; set; }
        public DbSet<Review> Review { get; set; }

       

     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<CCO>()
            .HasKey(t => new { t.CID, t.COID });

            modelBuilder.Entity<VehicleServiceCompany>()
            .HasMany(c => c.CCO)
            .WithRequired()
            .HasForeignKey(c => c.CID);

            modelBuilder.Entity<VehicleServiceCompanyOwner>()
            .HasMany(c => c.CCO)
            .WithRequired()
            .HasForeignKey(c => c.COID);

            modelBuilder.Entity<Service>()
            .HasOptional(x => x.Review).WithMany()
            .HasForeignKey(x => x.RID);

            modelBuilder.Entity<Review>()
                .HasRequired(x => x.Service).WithMany()
                .HasForeignKey(x => x.SID);




            modelBuilder.Entity<SE>()
          .HasKey(t => new { t.SID, t.EID });

            modelBuilder.Entity<Service>()
            .HasMany(c => c.SE)
            .WithRequired()
            .HasForeignKey(c => c.SID);

            modelBuilder.Entity<Employee>()
            .HasMany(c => c.SE)
            .WithRequired()
            .HasForeignKey(c => c.EID);




            

            modelBuilder.Entity<SSI>()
            .HasKey(t => new { t.SID, t.SIID });

            modelBuilder.Entity<Service>()
            .HasMany(c => c.SSI)
            .WithRequired()
            .HasForeignKey(c => c.SID);

            modelBuilder.Entity<ServiceIntervention>()
            .HasMany(c => c.SSI)
            .WithRequired()
            .HasForeignKey(c => c.SIID);


            modelBuilder.Entity<WorkingPoint>()
            .HasRequired<VehicleServiceCompany>(s => s.VehicleServiceCompany)
            .WithMany(g => g.WorkingPoints)
            .HasForeignKey<int>(s => s.VSCID)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
           .HasRequired<WorkingPoint>(s => s.WorkingPoint)
           .WithMany(g => g.Employees)
           .HasForeignKey<int>(s => s.WPID);
           

            modelBuilder.Entity<Service>()
         .HasRequired<WorkingPoint>(s => s.WorkingPoint)
         .WithMany(g => g.Services)
         .HasForeignKey<int>(s => s.WPID)
         .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
              .HasRequired<VehicleOwner>(s => s.VehicleOwner)
              .WithMany(g => g.Vehicles)
              .HasForeignKey<int>(s => s.OID);

            modelBuilder.Entity<Service>()
              .HasRequired<Vehicle>(s => s.Vehicle)
              .WithMany(g => g.Services)
              .HasForeignKey<int>(s => s.VID);

            modelBuilder.Entity<ServiceIntervention>()
              .HasRequired<WorkingPoint>(s => s.WorkingPoint)
              .WithMany(g => g.ServiceInterventions)
              .HasForeignKey<int?>(s => s.WP)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<ServiceIntervention>()
            .HasRequired<Currency>(s => s.Currency)
            .WithMany(g => g.ServiceIntervention)
            .HasForeignKey<int>(s => s.CID);

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
