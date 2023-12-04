using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealStateCompanyApplication.Context;

namespace RealStateCompanyTests
{
    public static class DatabaseConnectionTest
    {
        public static DbContextOptions<RealStateContext> GetConnection()
        {
            DbContextOptions<RealStateContext> _dbContextOptions;
            var builder = new DbContextOptionsBuilder<RealStateContext>();
            var connection = new SqlConnection("Data Source=LAPTOP-JN1EBVL9\\SQLEXPRESS;Initial Catalog=RealStateCompanyDb;Trusted_Connection=True;TrustServerCertificate=True");
            connection.Open();
            _dbContextOptions = builder.UseSqlServer(connection).Options;
            return _dbContextOptions;
        }
    }
}