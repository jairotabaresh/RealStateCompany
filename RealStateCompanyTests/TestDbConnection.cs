using RealStateCompanyApplication.Context;

namespace RealStateCompanyTests
{
    public class TestDbConnection
    {
        [Fact]
        public void CheckConnection()
        {
            RealStateContext context = new RealStateContext(DatabaseConnectionTest.GetConnection());

            Assert.NotNull(context);
        }
    }
}
