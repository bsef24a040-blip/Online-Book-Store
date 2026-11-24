using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly string _conStr;
        public DashboardRepository(IConfiguration config)
        {
            _conStr = config.GetConnectionString("DefaultConnection")!;
        }

        public int GetTotalBooksCount()
        {
            using var con = new SqlConnection(_conStr);
            return con.ExecuteScalar<int>("SELECT COUNT(*) FROM Books");
        }

        public int GetTotalAuthorsCount()
        {
            using var con = new SqlConnection(_conStr);
            return con.ExecuteScalar<int>("SELECT COUNT(*) FROM Authors");
        }
    }
}