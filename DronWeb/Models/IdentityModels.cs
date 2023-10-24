using System.Data.Entity;
using System.Reflection.Emit;
using DronWeb.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;

namespace DronWeb.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        
        public DbSet<Localization> Localizations { get; set; }
        
        public DbSet<Province> Provinces { get; set; }




        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // usuwa bład w enty framework z usuwanie tabeli w bazie danych
        {
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Localizations);


            base.OnModelCreating(modelBuilder);
        }
    }
}