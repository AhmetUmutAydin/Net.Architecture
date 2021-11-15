using System.Linq;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.Entities.Concrete.Auth;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Contexts
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<DomainEnum> DomainEnum { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    }
}
