using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SE.Identity.API.data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@$"
        //            Server={Environment.GetEnvironmentVariable("DBCONNECTION")};
        //            Database={Environment.GetEnvironmentVariable("DBNAME")};
        //            User={Environment.GetEnvironmentVariable("DBUSER")};
        //            Password={Environment.GetEnvironmentVariable("DBPASSWORD")};
        //            TrustServerCertificate=True
        //        ");
        //    }
        //}
    }
}
