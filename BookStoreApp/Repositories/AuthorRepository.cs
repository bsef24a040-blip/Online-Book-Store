using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BookStoreApp.Models;

namespace BookStoreApp.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _conStr;
        public AuthorRepository(IConfiguration config)
        {
            _conStr = config.GetConnectionString("DefaultConnection")!;
        }

        public List<Author> GetAllAuthors()
        {
            using var con = new SqlConnection(_conStr);
            return con.Query<Author>("SELECT * FROM Authors").ToList();
        }

        public void AddAuthor(Author author)
        {
            using var con = new SqlConnection(_conStr);
            con.Execute("INSERT INTO Authors (Name, Country) VALUES (@Name, @Country)", author);
        }
    }
}