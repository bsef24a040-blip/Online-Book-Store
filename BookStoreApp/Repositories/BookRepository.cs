using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _conStr;
        public BookRepository(IConfiguration config)
        {
            _conStr = config.GetConnectionString("DefaultConnection")!;
        }

        public List<BookCardViewModel> GetAllBooks()
        {
            using var con = new SqlConnection(_conStr);
            string sql = @"SELECT b.Id, b.Title, b.Price, a.Name AS AuthorName 
                           FROM Books b 
                           INNER JOIN Authors a ON b.AuthorId = a.Id";
            return con.Query<BookCardViewModel>(sql).ToList();
        }

        public void AddBook(Book book)
        {
            using var con = new SqlConnection(_conStr);
            con.Execute("INSERT INTO Books (Title, Price, AuthorId) VALUES (@Title, @Price, @AuthorId)", book);
        }

        public void DeleteBook(int id)
        {
            using var con = new SqlConnection(_conStr);
            con.Execute("DELETE FROM Books WHERE Id = @Id", new { Id = id });
        }
    }
}