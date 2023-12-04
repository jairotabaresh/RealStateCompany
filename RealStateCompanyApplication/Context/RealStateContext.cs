using Microsoft.EntityFrameworkCore;
using RealStateCompanyApplication.Model;

namespace RealStateCompanyApplication.Context
{
    /// <summary>
    /// Class used to establish the session with the database and define the different objects that represent the database tables
    /// </summary>
    public class RealStateContext: DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<User> Users { get; set; }

        public RealStateContext(DbContextOptions options) : base(options)
        {

        }
    }
}
