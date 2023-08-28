using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SE.Identidade.API.Models;

namespace SE.Identidade.API.data
{
    public class ApplicationDbContext : IdentityDbContext<UserRegister>
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
