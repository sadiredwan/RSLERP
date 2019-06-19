using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RSLERP.DataManager.Entity;
using System.Web;
using System.Data.Entity.Validation;

namespace RSLERP.DataManager
{
    public class DBContext : DbContext
    {
        public DBContext() : base("RSLERP_ConnectionString")
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, EFConsole..Configuration>());
        }
        public override int SaveChanges()
        {
            
                OnBeforeSaving();
                return base.SaveChanges();
          
        }

        public DbSet<InvSupplier> InvSuppliers { get; set; }

        public DbSet<InvSupplierType> InvSupplierTypes { get; set; }

        public DbSet<InvUnitMeasurement> InvUnitMeasurements { get; set; }

        public DbSet<InvManufacturer> InvManufacturers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<ProjectSegment> ProjectSegments { get; set; }

        public DbSet<BusinessSector> BusinessSectors { get; set; }

        public DbSet<BankInfo> BankInfos { get; set; }

        public DbSet<BankInfoType> BankInfoTypes { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Financialyear> Financialyears { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ApplicationState> ApplicationStates { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<CompanyUser> CompanyUsers { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<CompanyModule> CompanyModules { get; set; }

        public DbSet<CompanyUserMap> CompanyUserMaps { get; set; }

        public DbSet<AnFCOA> AnFCOAs { get; set; }

        private void OnBeforeSaving()
        {
            int user_id = 0;
            int app_id = 0;
            int company_id = 0;
            
            try
            {
                app_id = Convert.ToInt32(HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID]);
                ApplicationState appState = new DBContext().ApplicationStates.Find(app_id);
                user_id = appState.user_id;
                company_id = appState.company_id;
            }
            catch
            {

            }
            var entities = ChangeTracker.Entries();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseModel)entity.Entity).created_at = DateTime.Now;
                    ((IBaseModel)entity.Entity).created_by = user_id;
                }
                ((IBaseModel)entity.Entity).CompanyId = company_id;
                ((IBaseModel)entity.Entity).app_id = app_id;
                ((IBaseModel)entity.Entity).modified_at = DateTime.Now;
                ((IBaseModel)entity.Entity).modified_by = user_id;
            }

        }
    }
}
