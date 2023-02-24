using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class SimpleContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=batu\\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=True;");
            optionsBuilder.UseSqlServer("Server=BATU\\SQLEXPRESS;Database=Demo;Integrated Security=true;TrustServerCertificate=True;");
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
        public DbSet<EmailParameter>? EmailParameters { get; set; }

    }
}
